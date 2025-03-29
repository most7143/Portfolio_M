using System;
using System.Collections.Generic;

public static class EventManager<Enum>
{
    private static Dictionary<Enum, Delegate> eventActions = new Dictionary<Enum, Delegate>();

    public static void Register<TValue>(Enum eventType, Action<TValue> listener)
    {
        if (!eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = listener;
        }
        else
        {
            eventActions[eventType] = Delegate.Combine(eventActions[eventType], listener);
        }
    }

    public static void Register(Enum eventType, Action listener)
    {
        if (!eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = listener;
        }
        else
        {
            eventActions[eventType] = Delegate.Combine(eventActions[eventType], listener);
        }
    }

    public static void Unregister<TValue>(Enum eventType, Action<TValue> listener)
    {
        if (eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = Delegate.Remove(eventActions[eventType], listener);
        }
    }

    public static void Unregister(Enum eventType, Action listener)
    {
        if (eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = Delegate.Remove(eventActions[eventType], listener);
        }
    }

    public static void Send<TValue>(Enum eventType, TValue parameter)
    {
        if (eventActions.ContainsKey(eventType))
        {
            (eventActions[eventType] as Action<TValue>)?.Invoke(parameter);
        }
    }

    public static void Send(Enum eventType)
    {
        if (eventActions.ContainsKey(eventType))
        {
            (eventActions[eventType] as Action)?.Invoke();
        }
    }
}