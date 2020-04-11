using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class TowerCannon : MonoBehaviour
{
    [SerializeField] private CannonScriptable m_CannonScript;
    [SerializeField] private GameObjectScriptablePool m_BulletPool;
    private int m_Damage;
    private int m_NumberOfBullets;
    private float m_AttackRange;
    private float m_AttackSpeed;
    private Transform m_CurrentTarget;
    private DamageType m_DamageType;
    private GameObject m_Bullet;
    private List<Transform> m_Targets = new List<Transform>();
    private Player m_Player;
    private void Awake()
    {
        SetValues();
        SetupCollider();
        SetupRigidBody();
        InvokeRepeating(nameof(Shoot), 1, m_AttackSpeed);
    }

    private void SetupRigidBody()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void SetupCollider()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = m_AttackRange;
    }

    private void Update()
    {
        CalculateTarget();
    }
    private void Shoot()
    {
        if (m_Player.Health.Value <= 0)
        {
            CancelInvoke();
        }
        if (m_CurrentTarget != null && m_CurrentTarget.gameObject.activeSelf)
        {
            GameObject bullet = m_BulletPool.Rent(true);
            bullet.GetComponent<Bullet>().Throw(transform.position, m_CurrentTarget, m_Damage, m_DamageType);
        }
    }
    private void SetValues()
    {
        m_Player = FindObjectOfType<Player>();
        if (m_CannonScript != null)
        {
            m_Damage = m_CannonScript.Damage;
            m_NumberOfBullets = m_CannonScript.NumberOfBullets;
            m_AttackRange = m_CannonScript.AttackRange;
            m_AttackSpeed = m_CannonScript.AttackSpeed;
            m_DamageType = m_CannonScript.DamageType;
            m_Bullet = m_CannonScript.BulletPrefab;
        }

    }
    private void CalculateTarget()
    {
        if (m_Targets.Count == 0)
        {
            m_CurrentTarget = null;
            return;
        }

        else
        {
            float distance = m_CurrentTarget ? Vector3.Distance(m_CurrentTarget.transform.position, transform.position) : float.MaxValue;
            if (m_CurrentTarget != null)
            {
                if (Vector3.Distance(m_CurrentTarget.transform.position, transform.position) > m_AttackRange)
                {
                    m_CurrentTarget = null;
                }
            }

            foreach (Transform target in m_Targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < distance)
                {
                    m_CurrentTarget = target.transform;
                    transform.LookAt(m_CurrentTarget.position);
                }
            }
         
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (m_Targets.Contains(other.transform))
        {
            return;
        }
        else if (other.GetComponentInParent<Unit>())
        {
            other.GetComponentInParent<EmitOnDisable>().OnDisableGameObject += EnemyDisabled;
            m_Targets.Add(other.transform);
        }
    }
    private void EnemyDisabled(GameObject enemy)
    {
        for (int i = 0; i < m_Targets.Count; i++)
        {
            if (enemy.transform == m_Targets[i].parent.parent && m_Targets.Contains(m_Targets[i]))
            {
                m_Targets.Remove(m_Targets[i]);
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {       
        if (m_Targets.Contains(other.transform.parent))
        {
            other.GetComponentInParent<EmitOnDisable>().OnDisableGameObject -= EnemyDisabled;
            m_Targets.Remove(other.transform.parent);
        }
    }
}
