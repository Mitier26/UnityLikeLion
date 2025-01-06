using System;
using System.Collections.Generic;

public class Message<T>
{
    public T Data { get; private set; }

    public Message(T data)
    {
        Data = data;
    }
}

public interface IMessageQueue
{
    void ProcessMessages();
}

public class TypedQueue<T> : IMessageQueue
{
    private Queue<Message<T>> queue = new Queue<Message<T>>();
    private List<Action<T>> handlers = new List<Action<T>>();

    public void Enqueue(T data)
    {
        queue.Enqueue(new Message<T>(data));
    }

    public void AddHandler(Action<T> handler)
    {
        handlers.Add(handler);
    }

    public void RemoveHandler(Action<T> handler)
    {
        handlers.Remove(handler);
    }

    public void ProcessMessages()
    {
        while (queue.Count > 0)
        {
            var message = queue.Dequeue();
            foreach (var handler in handlers)
            {
                handler?.Invoke(message.Data);
            }
        }
    }
}

public class EventManager : Singleton<EventManager>
{
    private Dictionary<Type, IMessageQueue> messageQueues = new Dictionary<Type, IMessageQueue>();

    public void PushMessage<T>(T data)
    {
        GetQueue<T>().Enqueue(data);
    }

    public void Subscribe<T>(Action<T> handler)
    {
        GetQueue<T>().AddHandler(handler);
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        GetQueue<T>().RemoveHandler(handler);
    }

    private TypedQueue<T> GetQueue<T>()
    {
        var type = typeof(T);
        if (!messageQueues.ContainsKey(type))
        {
            messageQueues[type] = new TypedQueue<T>();
        }
        return messageQueues[type] as TypedQueue<T>;
    }

    private void Update()
    {
        foreach (var queue in messageQueues.Values)
        {
            queue.ProcessMessages();
        }
    }
}