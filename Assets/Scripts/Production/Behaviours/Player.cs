using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ObservableProperty<int> m_Health = new ObservableProperty<int>();

    private string m_Name;
    public event Action<string> OnNameChanged;
    public string Name
    {
        get => m_Name;
        set
        {
            if (m_Name != value)
            {
                m_Name = value;
                OnNameChanged?.Invoke(m_Name);
            }
        }
    }

    [ContextMenu("Increase Health")]
    public void Increase()
    {
        m_Health.Value += 1;
    }
}
