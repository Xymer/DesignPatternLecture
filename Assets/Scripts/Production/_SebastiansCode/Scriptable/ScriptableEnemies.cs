using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/Enemies/Enemy"))]
public class ScriptableEnemies : ScriptableObject
{
    [SerializeField] private float m_MovementSpeed = 1;
    [SerializeField] private int m_Health = 10;

    [SerializeField] private GameObject m_Prefab;
    private int m_CurrentPath = 0;
    public float MovementSpeed
    {
        get => m_MovementSpeed;

    }
    public int Health
    {
        get => m_Health;
    }
    public GameObject Prefab
    {
        get => m_Prefab;
    }
    public int CurrentPath
    {
        get => m_CurrentPath;
        set
        {
            if (m_CurrentPath != value)
            {
                m_CurrentPath = value;
            }
        }
    }

}
