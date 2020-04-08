using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private List<Vector3> m_Path;
    private int m_CurrentPath;
    private float m_MoveSpeed;
    private int m_Health;

    
    private void OnEnable()
    {
        Reset();
        StartMoving();
    }

    private void Reset()
    {
        
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
        return convertedPath;
    }
    public void GetPath(IEnumerable<Vector2Int> path)
    {
        m_Path = (List<Vector3>)ConvertPathToVector3(path);
    }
    private void StartMoving()
    {
        InvokeRepeating("Move", 1, m_MoveSpeed);
    }
    private void Move()
    {
        m_CurrentPath++;
        transform.position = m_Path[m_CurrentPath];
    }
}
