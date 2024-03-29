﻿using System;
using System.Collections.Generic;

public static class ObservableExtensions
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
    {
        IObserver<T> observer = new ActionToObserver<T>(onNext);
        return observable.Subscribe(observer);
    }

    public static IDisposable Subscribe<T>(this IObservable<T> observable, IObserver<T> observer)
    {

        return observable.Subscribe(observer);

    }
}

public class SkipFirstNotificationObserver<T> : IObserver<T>
{
    private bool m_IsFirstTime = true;
    private readonly IObserver<T> m_InnerObserver;

    public SkipFirstNotificationObserver(IObserver<T> innerObserver)
    {
        m_InnerObserver = innerObserver;
    }

    public void OnCompleted() { }
    public void OnError(Exception error) { }

    public void OnNext(T value)
    {
        if (m_IsFirstTime)
        {
            m_IsFirstTime = false;
        }
        else
        {
            m_InnerObserver.OnNext(value);
        }
    }
}
public class SkipOperator<T> : IObservable<T>, IObserver<T>
{
    private int m_Skip;
    private IObservable<T> m_OriginalObservable;
    public SkipOperator(IObservable<T> originalObservable, int skip)
    {
        m_OriginalObservable = originalObservable;
        m_Skip = skip;
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

    public IDisposable Subscribe(IObserver<T> observer)
    {
        SkipObserver<T> skipObserver = new SkipObserver<T>(this, observer, m_Skip);
        return m_OriginalObservable.Subscribe(skipObserver);
    }
}

public class SkipObserver<T> : IObserver<T>
{
    private SkipOperator<T> m_Parent;
    private readonly IObserver<T> m_OriginalObserver;
    private readonly int m_Skip;
    private int m_CurrentSkips;

    public SkipObserver(SkipOperator<T> parent, IObserver<T> originalObserver, int skip)
    {
        m_Parent = parent;
        m_OriginalObserver = originalObserver;
        m_Skip = skip;
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



public class ActionToObserver<T> : IObserver<T>
{
    private readonly Action<T> m_Action;

    public ActionToObserver(Action<T> action)
    {
        m_Action = action;
    }

    public void OnCompleted() { }
    public void OnError(Exception error) { }

    public void OnNext(T value)
    {
        m_Action.Invoke(value);
    }
}
public class ObservableProperty<T> : IObservable<T>
{
    private bool m_IsInitialised;
    private T m_Value;
    private readonly Subject<T> m_Subject = new Subject<T>();

    public T Value
    {
        get => m_Value;
        set
        {
            m_IsInitialised = true;
            if (EqualityComparer<T>.Default.Equals(m_Value, value) == false) // value == m_Value
            {
                m_Value = value;
                m_Subject.OnNext(m_Value);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        IDisposable subscription = null;
        try
        {
            if (m_IsInitialised)
            {
                observer.OnNext(m_Value);
            }
        }
        finally
        {
            subscription = m_Subject.Subscribe(observer);
        }
        return subscription;
    }
}

public interface ISubject<T> : IObservable<T>, IObserver<T> { }
public class Subject<T> : ISubject<T>
{
    private int m_Index = 0;

    private readonly List<Subscriber<T>> m_Observers = new List<Subscriber<T>>();

    public void OnCompleted()
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnCompleted();
        }
    }

    public void OnError(Exception error)
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnCompleted();
        }
    }

    public void OnNext(T value)
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnNext(value);
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        m_Observers.Add(new Subscriber<T>(observer));
        return new Subscription(this, observer);
    }

    private class Subscription : IDisposable
    {
        private readonly Subject<T> m_Subject;
        private readonly IObserver<T> m_Observer;

        public Subscription(Subject<T> subject, IObserver<T> observer)
        {
            m_Subject = subject;
            m_Observer = observer;
        }

        public void Dispose()
        {
            //int elementIndex = m_Subject.m_Observers.IndexOf(m_Observer);
            //if (elementIndex >= 0)
            //{
            //    m_Subject.m_Observers.Remove(m_Observer);

            //    if (elementIndex <= m_Subject.m_Index)
            //    {
            //        m_Subject.m_Index--;
            //    }
            //}
        }
    }
}
public class Subscriber<T> : IObserver<T>, IDisposable
{
    private readonly IObserver<T> m_Observer;

    public bool Unsubscribed { get; set; }

    public Subscriber(IObserver<T> observer)
    {
        m_Observer = observer;
    }

    public void OnCompleted()
    {
        m_Observer.OnCompleted();
    }

    public void OnError(Exception error)
    {
        m_Observer.OnError(error);
    }

    public void OnNext(T value)
    {
        m_Observer.OnNext(value);
    }

    public void Dispose()
    {
        Unsubscribed = true;
    }
}
public class SubjectCaller
{
    public void SubscribeToSubject()
    {
        Subject<int> intStream = new Subject<int>();
        //IObservable<int> abstractIntStream = intStream;
       // IDisposable subscription = (Subject<int>.Unsubscriber)intStream.Subscribe(null);
        //subscription.Dispose();
    }
}

