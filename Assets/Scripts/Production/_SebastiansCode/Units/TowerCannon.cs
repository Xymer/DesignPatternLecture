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
        if (m_CurrentTarget != null && m_CurrentTarget.gameObject.activeSelf)
        {
            GameObject bullet = m_BulletPool.Rent(true);
            bullet.GetComponent<Bullet>().Throw(transform.position, m_CurrentTarget);
        }
    }
    private void SetValues()
    {
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
            float distance = float.MaxValue;
            foreach (Transform target in m_Targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < distance)
                {
                    m_CurrentTarget = target.transform.parent;
                    transform.LookAt(m_CurrentTarget.position);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (m_Targets.Contains(other.transform.parent))
        {
            return;
        }
        if (other.GetComponentInParent<Unit>())
        {
            other.GetComponentInParent<EmitOnDisable>().OnDisableGameObject += EnemyDisabled;
            m_Targets.Add(other.transform.parent);
        }
    }
    private void EnemyDisabled(GameObject enemy)
    {

            m_Targets.Remove(enemy.transform.parent);
            if (m_CurrentTarget == enemy.transform.parent)
            {
                m_CurrentTarget = null;
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
