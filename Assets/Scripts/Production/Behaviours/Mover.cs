using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mover : MonoBehaviour, IObserver<int>
{
    private Player m_Player;
    
    private void Start()
    {
        m_Player = FindObjectOfType<Player>();
        m_Player.Health.Subscribe(this);
    }

    public void OnCompleted()
    {
        
    }

    public void OnError(Exception error)
    {
        
    }

    public void OnNext(int value)
    {
        transform.position+= Random.onUnitSphere;
    }
}
