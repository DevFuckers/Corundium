using System;
using System.Collections.Generic;
using CodeBase;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus
{
    public class EventBus : IDisposable
    {
        private readonly Dictionary<Event, Action> _events = new();

        public void Subscribe(Event eventType, Action action)
        {
            _events.TryAdd(eventType, delegate { });

            _events[eventType] += action;
        }

        public void Unsubscribe(Event eventType, Action action)
        {
            if (_events.ContainsKey(eventType))
                _events[eventType] -= action;
        }

        public void Trigger(Event eventType)
        {
            // Используем TryGetValue для безопасности
            if (_events.TryGetValue(eventType, out var action))
                action?.Invoke();
        }
        
        public void Dispose()
        {
            _events.Clear();
        }
    }
}