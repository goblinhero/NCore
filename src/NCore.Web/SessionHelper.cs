using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using NCore.Extensions;
using NCore.Web.Aspects;
using NCore.Web.Commands;
using NCore.Web.Queries;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;
using Configuration = NHibernate.Cfg.Configuration;
using Serilog;
namespace NCore.Web
{
    public class SessionHelper
    {
        public class CompanyFilterDef : FilterDefinition
        {
            public CompanyFilterDef()
                : base(CompanyFilter, "CompanyId = :companyId", new Dictionary<string, IType> {{ "companyId", NHibernateUtil.Int64}}, false)
            {
                
            }        }
        private static ISessionFactory _sessionFactory;
        private static bool _initialized;
        public const string CompanyFilter = "CompanyFilter";
        private readonly IList<Action<ISession>> _createSteps = new List<Action<ISession>>();

        public static bool TryInitialize(ConnectionStringSettings connection, out IEnumerable<string> errors, DBSettings settings = null, params Type[] mappingTypes)
        {
            try
            {
                Log.Information("Starting initialization of nHibernate SessionFactory");
                settings = settings ?? DBSettings.Default;
                var configuration = new Configuration();
                configuration.SessionFactoryName("BuildIt");
                configuration.DataBaseIntegration(db =>
                {
                    db.Dialect<MsSql2012Dialect>();
                    db.Driver<SqlClientDriver>();
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.IsolationLevel = settings.IsolationLevel;
                    db.ConnectionString = connection.ConnectionString;
                });
                var modelMapper = new ModelMapper();
                mappingTypes.ForEach(t => modelMapper.AddMappings(Assembly.GetAssembly(t).GetExportedTypes()));
                modelMapper.BeforeMapManyToOne += (modelInspector, propertyPath, map) => map.Column(propertyPath.LocalMember.Name + "Id");
                modelMapper.BeforeMapProperty += (inspector, member, customizer) =>
                {
                    if (member.GetRootMember().MemberType == MemberTypes.Property &&
                        ((PropertyInfo)member.GetRootMember()).PropertyType == typeof(DateTime))
                    {
                        customizer.Type<UtcDateTimeType>();
                    }
                };
                configuration.AddFilterDefinition(new CompanyFilterDef());
                var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
                configuration.AddDeserializedMapping(mapping, "mappings");
                var triggerConfig = new TriggerConfig();
                configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { triggerConfig };
                configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { triggerConfig };
                configuration.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] { triggerConfig };
                configuration.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[] { triggerConfig };
                configuration.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[] { triggerConfig };
                _sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                errors = new[]
                {
                    $"{ex.Message} - connecting to: {connection.ConnectionString}",
                    $"Stacktrace: {ex.StackTrace}"
                };
                Log.Error(ex, $"Initializing SessionFactory failed.");
                errors.ForEach(Log.Error);
                return false;
            }
            _initialized = true;
            Log.Information("Initialization of nHibernate SessionFactory completed.");
            errors = new string[0];
            return true;
        }

        private ISession OpenSession()
        {
            var session = _sessionFactory.OpenSession();
            _createSteps.ForEach(a => a(session));
            return session;
        }
        public bool TryQuery<T>(IQuery<T> query, out T result, out IEnumerable<string> errors)
        {
            if (!_initialized)
            {
                result = default(T);
                return this.Error(out errors,
                    "SessionFactory has not been initialized. Did you forget to call SessionHelper.TryInitialize()?");
            }
            using (var session = OpenSession())
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    if (!query.TryExecute(session, out result, out errors))
                    {
                        Log.Debug($"Query {query.GetType().Name} failed with the following errors:");
                        errors.ForEach(Log.Debug);
                        tx.Commit();
                        return false;
                    }
                    tx.Commit();
                    return this.Success(out errors);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Query { query.GetType().Name} caused an exception.");
                    result = default(T);
                    return ex.Error(out errors);
                }
            }
        }

        public bool TryExecute(ICommand command, out IEnumerable<string> errors)
        {
            if (!_initialized)
            {
                return this.Error(out errors,
                    "SessionFactory has not been initialized. Did you forget to call SessionHelper.TryInitialize()?");
            }
            using (var session = OpenSession())
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    var result = command.TryExecute(session, out errors);
                    tx.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Query { command.GetType().Name} caused an exception.");
                    return ex.Error(out errors);
                }
            }
        }

        public class DBSettings
        {
            private DBSettings()
            {
                IsolationLevel = IsolationLevel.ReadCommitted;
            }

            public DBSettings(IsolationLevel isolationLevel)
            {
                IsolationLevel = isolationLevel;
            }

            public static DBSettings Default => new DBSettings();
            public IsolationLevel IsolationLevel { get; }
        }

        public void AddSessionCreateStep(Action<ISession> create)
        {
            _createSteps.Add(create);
        }
    }
}