using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private int health;

    public event Action<int> OnPlayerHealthChanged;
    public int Health
    {
        get => health;
        set
        {
            if (health != value)
            {
                health = value;
                OnPlayerHealthChanged?.Invoke(health);
            }
        }
    }

    private string name;
    public event Action<string> OnNameChanged;
    public string Name
    {
        get => name;
        set
        {
            if(name != value)
            {
                name = value;
                OnNameChanged?.Invoke(name);
            }
        }
    }

    [ContextMenu("Increase Health")]
    public void Increase()
    {
        health += 1;
    }
}
