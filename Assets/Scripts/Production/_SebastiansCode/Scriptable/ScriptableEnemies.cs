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
    private List<Vector2Int> m_Path;
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
    public List<Vector2Int> Path
    {
        get => m_Path;
        set => m_Path = value;
    }
    public void Move()
    {
        Vector3 moveTo = new Vector3(m_Path[CurrentPath].x, 1, m_Path[CurrentPath].y);
        m_Prefab.transform.position = moveTo;
    }
}
