using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]private ScriptableEnemies m_Scriptable;
    private List<Vector3> m_Path;
    private int m_CurrentPath = 0;
    private int m_Health = 10;
    private int m_Damage = 1;
    private float m_MoveSpeed = 1.75f;
    private bool m_ListConverted = false;
    
    private void OnEnable()
    {
        SetValues();
        StartMoving();
    }
    private void OnDisable()
    {
        SetValues();
    }
    private void Update()
    {
        if (m_Health <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }
    private void Reset()
    {
        m_CurrentPath = 0;
        m_Health = 10;
    }
    private IEnumerable<Vector3> ConvertPathToVector3(IEnumerable<Vector2Int> path)
    {
        List<Vector3> convertedPath = new List<Vector3>();
        Vector3 currentPos = Vector3.zero;       
        foreach (Vector2Int pos in path)
        {
            currentPos = new Vector3(pos.x, 1, pos.y);
            convertedPath.Add(currentPos);
        }
        if (m_ListConverted)
        {
            return m_Path;
        }
        else
        { 
        m_ListConverted = true;
        return convertedPath;
        }
    }
    public void GetPath(IEnumerable<Vector2Int> path)
    {
        m_Path = (List<Vector3>)ConvertPathToVector3(path);
    }
    private void StartMoving()
    {
        InvokeRepeating("Move", 1,m_MoveSpeed);
    }
    private void Move()
    {
       
        m_CurrentPath++;
        if (m_CurrentPath == m_Path.Count)
        {
            CancelInvoke("Move");
            gameObject.SetActive(false);
        }
        else
        {
        transform.LookAt(m_Path[m_CurrentPath]);
        transform.position = m_Path[m_CurrentPath];
        }
        
    }
    public void SetValues()
    {
        m_MoveSpeed = m_Scriptable.MovementSpeed;
        m_Health = m_Scriptable.Health;
        m_Damage = m_Scriptable.Damage;
        m_CurrentPath = 0;
    }

}
