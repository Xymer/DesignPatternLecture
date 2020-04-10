using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/Enemies/Enemy"))]
public class EnemyScriptable : ScriptableObject
{
    [SerializeField] private float m_MovementSpeed = 1;
    [SerializeField] private int m_Health = 10;
    [SerializeField] private int m_Damage = 1;
    [SerializeField] private GameObject m_Prefab;
    

    public float MovementSpeed
    {
        get => m_MovementSpeed;

    }
    public int Damage
    {
        get => m_Damage;

        set
        {
            if (m_Damage != value)
            {
                m_Damage = value;
            }
        }
    }
    public int Health
    {
        get => m_Health;
    }
    public GameObject Prefab
    {
        get => m_Prefab;
    }


}
