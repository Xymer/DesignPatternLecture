using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ObservableProperty<int> health = new ObservableProperty<int>();

    private string name;
    public event Action<string> OnNameChanged;
    public string Name
    {
        get => name;
        set
        {
            if (name != value)
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
