using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_MinSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private Rigidbody m_Rigidbody;
    private int m_Damage;
    private DamageType m_DamageType;
    private Vector3 m_TargetPosition;
  

    public void Reset()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }
    private void Update()
    {

    }
    private void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Reset();
        CancelInvoke(nameof(Sleep));
    }
    public void Throw(Vector3 startPosition, Transform target,int damage, DamageType damageType)
    {
        m_Damage = damage;
        m_DamageType = damageType;
        transform.position = startPosition;

        m_Rigidbody.AddForce(((target.position - transform.position  ).normalized) * m_MaxSpeed);
        Invoke(nameof(Sleep), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Unit>())
        {
            Unit unit = other.GetComponentInParent<Unit>();
            unit.TakeDamage(m_Damage, m_DamageType);
            gameObject.SetActive(false);
        }
    }
}
