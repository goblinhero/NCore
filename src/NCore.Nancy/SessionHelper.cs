using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using NCore.Extensions;
using NCore.Nancy.Aspects;
using NCore.Nancy.Commands;
using NCore.Nancy.Queries;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Type;
using Configuration = NHibernate.Cfg.Configuration;

namespace NCore.Nancy
{
    public class SessionHelper
    {
        private static ISessionFactory _sessionFactory;
        private static bool _initialized;

        public static bool TryInitialize(ConnectionStringSettings connection, out IEnumerable<string> errors,
            DBSettings settings = null, params Type[] mappingTypes)
        {
            try
            {
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
                modelMapper.BeforeMapManyToOne +=
                    (modelInspector, propertyPath, map) => map.Column(propertyPath.LocalMember.Name + "Id");
                modelMapper.BeforeMapProperty += (inspector, member, customizer) =>
                {
                    if (member.GetRootMember().MemberType == MemberTypes.Property &&
                        ((PropertyInfo) member.GetRootMember()).PropertyType == typeof(DateTime))
                    {
                        customizer.Type<UtcDateTimeType>();
                    }
                };
                var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
                configuration.AddDeserializedMapping(mapping, "mappings");
                var triggerConfig = new TriggerConfig();
                configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] {triggerConfig};
                configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] {triggerConfig};
                configuration.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] {triggerConfig};
                configuration.EventListeners.PostInsertEventListeners = new IPostInsertEventListener[] {triggerConfig};
                configuration.EventListeners.PostUpdateEventListeners = new IPostUpdateEventListener[] {triggerConfig};
                _sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                errors = new[]
                {
                    $"{ex.Message} - connecting to: {connection.ConnectionString}",
                    $"Stacktrace: {ex.StackTrace}"
                };
                return false;
            }
            _initialized = true;
            errors = new string[0];
            return true;
        }

        public bool TryQuery<T>(IQuery<T> query, out T result, out IEnumerable<string> errors)
        {
            using (var session = _sessionFactory.OpenStatelessSession())
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    if (!query.TryExecute(session, out result, out errors))
                    {
                        tx.Commit();
                        return false;
                    }
                    tx.Commit();
                    return this.Success(out errors);
                }
                catch (Exception ex)
                {
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
            using (var session = _sessionFactory.OpenSession())
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
    }
}