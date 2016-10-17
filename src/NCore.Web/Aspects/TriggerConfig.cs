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
        private static readonly List<IStrategy<PreInsertEvent>> _preInsertStrategies = new List<IStrategy<PreInsertEvent>>();
        private static readonly List<IStrategy<PreUpdateEvent>> _preUpdateStrategies = new List<IStrategy<PreUpdateEvent>>();
        private static readonly List<IStrategy<PreDeleteEvent>> _preDeleteStrategies = new List<IStrategy<PreDeleteEvent>>();
        private static readonly List<IStrategy<PostInsertEvent>> _postInsertStrategies = new List<IStrategy<PostInsertEvent>>();
        private static readonly List<IStrategy<PostUpdateEvent>> _postUpdateStrategies = new List<IStrategy<PostUpdateEvent>>();

        public void OnPostInsert(PostInsertEvent ev)
        {
            _postInsertStrategies.Execute(ev);
        }

        public void OnPostUpdate(PostUpdateEvent ev)
        {
            _postUpdateStrategies.Execute(ev);
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

        public static void AddPreInsertStrategies(params IStrategy<PreInsertEvent>[] strategies)
        {
            _preInsertStrategies.AddRange(strategies);
        }

        public static void AddPreUpdateStrategies(params IStrategy<PreUpdateEvent>[] strategies)
        {
            _preUpdateStrategies.AddRange(strategies);
        }

        public static void AddPreDeleteStrategies(params IStrategy<PreDeleteEvent>[] strategies)
        {
            _preDeleteStrategies.AddRange(strategies);
        }

        public static void AddPostInsertStrategies(params IStrategy<PostInsertEvent>[] strategies)
        {
            _postInsertStrategies.AddRange(strategies);
        }

        public static void AddPostUpdateStrategies(params IStrategy<PostUpdateEvent>[] strategies)
        {
            _postUpdateStrategies.AddRange(strategies);
        }
    }
}