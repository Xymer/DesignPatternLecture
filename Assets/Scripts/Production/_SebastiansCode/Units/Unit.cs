using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private EnemyScriptable m_Scriptable;
    private List<Vector3> m_Path;
    private Player m_Player;
    private int m_CurrentPath = 0;
    private float m_MoveSpeed = 1.75f;
    private int m_Health = 10;
   
    private bool m_IsFrozen = false;

    private void OnEnable()
    {
        SetValues();
        StartMoving();
    }
    private void OnDisable()
    {
        Reset();
    }
    private void Update()
    {

    }
    private void Reset()
    {
        m_CurrentPath = 0;
        m_Health = m_Scriptable.Health;
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
    public void SetPath(IEnumerable<Vector2Int> path)
    {
        m_Path = (List<Vector3>)ConvertPathToVector3(path);
    }
    private void StartMoving()
    {
        InvokeRepeating(nameof(Move), 1, m_MoveSpeed);
      
    }
    private void Move()
    {
        m_CurrentPath++;
        if (m_Player.Health.Value <= 0)
        {
            CancelInvoke();
            
        }
        if (m_CurrentPath == m_Path.Count)
        {
            CancelInvoke(nameof(Move));
           
            m_Player.Health.Value--;
            
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
        m_Player = FindObjectOfType<Player>();
        m_MoveSpeed = m_Scriptable.MovementSpeed;
        m_Health = m_Scriptable.Health;
        m_CurrentPath = 0;
    }
    public void TakeDamage(int damage, DamageType damageType)
    {
        m_Health -= damage;
        if (!m_IsFrozen && damageType == DamageType.frost)
        {
            m_IsFrozen = true;
            CancelInvoke(nameof(Move));
            InvokeRepeating(nameof(Move), 1, m_MoveSpeed * 1.5f);
        }
        if (m_Health <= 0)
        {
            CancelInvoke(nameof(Move));

            gameObject.SetActive(false);
        }
    }

}
