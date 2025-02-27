using System;
using System.Collections.Generic;

public static class EventManager<T> where T : Enum
{
    // �̺�Ʈ Ÿ�Ժ��� �����ʸ� �����ϴ� ��ųʸ� (�Ű����� �ִ� �Լ���)
    private static Dictionary<T, Delegate> eventActions = new Dictionary<T, Delegate>();

    // �̺�Ʈ ��� (�Ű������� �ִ� �̺�Ʈ)
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

    // �̺�Ʈ ����
    public static void Unregister<TValue>(T eventType, Action<TValue> listener)
    {
        if (eventActions.ContainsKey(eventType))
        {
            eventActions[eventType] = Delegate.Remove(eventActions[eventType], listener);
        }
    }

    // �̺�Ʈ �߻� (�Ű����� �ִ� �̺�Ʈ)
    public static void Send<TValue>(T eventType, TValue parameter)
    {
        if (eventActions.ContainsKey(eventType))
        {
            // �̺�Ʈ Ÿ�Կ� �´� ������ ȣ��
            (eventActions[eventType] as Action<TValue>)?.Invoke(parameter);
        }
    }

    // �Ű����� ���� �̺�Ʈ (�ʿ�� ����)
    public static void Send(T eventType)
    {
        if (eventActions.ContainsKey(eventType))
        {
            (eventActions[eventType] as Action)?.Invoke();
        }
    }
}