using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageType
{
    normal = 0,
    frost = 1
}
[CreateAssetMenu(menuName = ("ScriptableObject/Towers/Cannon"))]
public class CannonScriptable : ScriptableObject
{
    [SerializeField] private int m_Damage;
    [SerializeField] private int m_NumberOfBullets;
    [SerializeField] private float m_AttackRange;
    [SerializeField] private float m_AttackSpeed;
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private DamageType m_DamageType;

    public GameObject BulletPrefab
    {
        get => m_BulletPrefab;
        private set { }
    }
    public int Damage
    {
        get => m_Damage;
        private set { }
    }
    public DamageType DamageType
    {
        get => m_DamageType;
        private set { }
    }

    public int NumberOfBullets 
    { 
        get => m_NumberOfBullets; 
        private set { } 
    }

    public float AttackRange 
    { 
        get => m_AttackRange;
        private set { }
    }
    public float AttackSpeed 
    { 
        get => m_AttackSpeed;
        private set {} }
}

