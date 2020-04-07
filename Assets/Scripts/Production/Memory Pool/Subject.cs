using System;
using System.Collections.Generic;

public static class IObservableExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable,Action<T> onNext)
    {
        return observable.Subscribe(new ActionToObserver<T>(onNext));
    }
}
public class ActionToObserver<T> : IObserver<T>
{
    private Action<T> action;
    public ActionToObserver(Action<T> action)
    {
        this.action = action;
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(T value)
    {
    }
}
public class ObservableProperty<T> : IObservable<T>
{
    private T m_Value;
    private Subject<T> subject = new Subject<T>();

    public T Value
    {
        get => m_Value;

        set
        {
            if (!EqualityComparer<T>.Default.Equals(m_Value,value))
            {
                m_Value = value;
                subject.OnNext(m_Value);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
      return  subject.Subscribe(observer);
    }
}

public interface ISubject<T> : IObservable<T>, IObserver<T> { }
public class Subject<T> : IObservable<T>, IObserver<T> 
{
    //Collection of listeners that are notified when the event is raised.

    List<IObserver<T>> observers = new List<IObserver<T>>();
    public void OnCompleted()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnCompleted();
        }
    }

    public void OnError(Exception error)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnError(error);
        }
    }

    public void OnNext(T value)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNext(value);
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        observers.Add(observer);
        return new Unsubscriber(observers,observer);
    }

    public class Unsubscriber : IDisposable
    {
        List<IObserver<T>> observers;
        private IObserver<T> observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }

}
public class SubjectCaller
{
    public void SubscribeToSubject()
    {
        Subject<int> intStream = new Subject<int>();
        //IObservable<int> abstractIntStream = intStream;
        IDisposable subscription = (Subject<int>.Unsubscriber)intStream.Subscribe(null);
        subscription.Dispose();
    }
}

