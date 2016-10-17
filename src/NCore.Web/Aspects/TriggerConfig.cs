using System.Collections.Generic;
using NCore.Extensions;
using NCore.Web.Aspects.Strategies;
using NCore.Strategies;
using NHibernate.Event;

namespace NCore.Web.Aspects
{
    public class TriggerConfig : IPreInsertEventListener, IPreUpdateEventListener, IPreDeleteEventListener,
        IPostInsertEventListener, IPostUpdateEventListener
    {
        private static readonly List<IStrategy<PreInsertEvent>> _preInsertStrategies =
            new List<IStrategy<PreInsertEvent>>();

        private static readonly List<IStrategy<PreUpdateEvent>> _preUpdateStrategies =
            new List<IStrategy<PreUpdateEvent>>();

        private static readonly List<IStrategy<PreDeleteEvent>> _preDeleteStrategies =
            new List<IStrategy<PreDeleteEvent>>();

        public void OnPostInsert(PostInsertEvent ev)
        {
        }

        public void OnPostUpdate(PostUpdateEvent ev)
        {
        }

        public bool OnPreDelete(PreDeleteEvent ev)
        {
            _preDeleteStrategies.Execute(ev);
            return false;
        }

        public bool OnPreInsert(PreInsertEvent ev)
        {
            _preInsertStrategies.Execute(ev);
            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent ev)
        {
            _preUpdateStrategies.Execute(ev);
            return false;
        }

        public static void InitializeDefault()
        {
            _preInsertStrategies.Add(new SetEntityCreationDateStrategy());
            _preInsertStrategies.Add(new SetTransactionCreationDateStrategy());
            _preInsertStrategies.Add(new ValidStrategy());
            _preUpdateStrategies.Add(new ValidStrategy());
            _preDeleteStrategies.Add(new DisallowTransactionUpdateStrategy());
        }

        public static void AddPreInsertStrategy(IStrategy<PreInsertEvent> strategy)
        {
            _preInsertStrategies.Add(strategy);
        }

        public static void AddPreUpdateStrategy(IStrategy<PreUpdateEvent> strategy)
        {
            _preUpdateStrategies.Add(strategy);
        }

        public static void AddPreDeleteStrategy(IStrategy<PreDeleteEvent> strategy)
        {
            _preDeleteStrategies.Add(strategy);
        }
    }
}