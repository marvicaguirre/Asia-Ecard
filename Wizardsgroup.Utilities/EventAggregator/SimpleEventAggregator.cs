using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Utilities.EventAggregator
{
    public class SimpleEventAggregator : IEventAggregator
    {
        #region Members
        private readonly Dictionary<Type, List<object>> _eventSubscriberLists =
            new Dictionary<Type, List<object>>();
        private readonly object _lock = new object(); 
        #endregion

        #region Public Functions
        /// <summary>
        /// Subscribes the specified subscriber.
        /// Subcriber must implement ISubscriber<!--<TEventArg>-->
        /// TEventArg must implement ICustomEntityArg<!--<TEntity>-->        
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(object subscriber)
        {
            lock (_lock)
            {
                var subscriberTypes = subscriber.GetType()
                    .GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                //var weakReference = new WeakReference(subscriber);
                foreach (var subscribers in subscriberTypes.Select(_GetSubscribers))
                {
                    subscribers.Add(subscriber);
                }
            }
        }
        /// <summary>
        /// Publishes the specified sender.
        /// </summary>
        /// <typeparam name="TEventArg">The type of the event arg.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="eventToPublish">The event to publish.</param>
        public void Publish<TEventArg>(object sender, TEventArg eventToPublish)
        {
            var subscriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventArg));
            var subscribers = _GetSubscribers(subscriberType);
            var subscribersToRemove = new List<object>();

            foreach (var weakSubscriber in subscribers)
            {
                var subscriber = weakSubscriber as ISubscriber<TEventArg>;
                if (subscriber != null)
                {                    
                    subscriber.OnEvent(sender, eventToPublish);
                    //Triggering of event is delayed using thread. :(...
                    //var syncContext = SynchronizationContext.Current ?? new SynchronizationContext();
                    //syncContext.Post(s => subscriber.OnEvent(eventToPublish), null);
                }
                else
                {
                    subscribersToRemove.Add(weakSubscriber);
                }
            }

            if (subscribersToRemove.Any())
            {
                lock (_lock)
                {
                    foreach (var remove in subscribersToRemove)
                        subscribers.Remove(remove);
                }
            }
        } 
        #endregion

        #region Private Functions
        private List<object> _GetSubscribers(Type subscriberType)
        {
            List<object> subscribers;
            lock (_lock)
            {
                var found = _eventSubscriberLists.TryGetValue(subscriberType, out subscribers);
                if (!found)
                {
                    subscribers = new List<object>();
                    _eventSubscriberLists.Add(subscriberType, subscribers);
                }
            }
            return subscribers;
        } 
        #endregion
    }
}
