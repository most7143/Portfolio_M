using System;
using System.Collections.Generic;

public static class EventManager<T> where T : Enum
{
    // 이벤트 타입별로 리스너를 관리하는 딕셔너리 (매개변수 있는 함수들)
    private static Dictionary<T, Delegate> eventActions = new Dictionary<T, Delegate>();

    // 이벤트 등록 (매개변수가 있는 이벤트)
    public static void Register<TValue>(T eventType, Action<TValue> listener)
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

    // 이벤트 해제
    public static void Unregister<TValue>(T eventType, Action<TValue> listener)
    {
        if (eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = Delegate.Remove(eventActions[eventType], listener);
        }
    }

    // 이벤트 발생 (매개변수 있는 이벤트)
    public static void Send<TValue>(T eventType, TValue parameter)
    {
        if (eventActions.ContainsKey(eventType))
        {
            // 이벤트 타입에 맞는 리스너 호출
            (eventActions[eventType] as Action<TValue>)?.Invoke(parameter);
        }
    }

    // 매개변수 없는 이벤트 (필요시 구현)
    public static void Send(T eventType)
    {
        if (eventActions.ContainsKey(eventType))
        {
            (eventActions[eventType] as Action)?.Invoke();
        }
    }
}