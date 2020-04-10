using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_MinSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private Rigidbody m_Rigidbody;
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
    public void Throw(Vector3 startPosition, Transform target)
    {       
        transform.position = startPosition;

        m_Rigidbody.AddForce(((target.position - transform.position  ).normalized) * m_MaxSpeed);
        Invoke(nameof(Sleep), 1.5f);
    }

}
