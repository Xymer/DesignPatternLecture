using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_MinSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private Rigidbody m_Rigidbody;

    public void Reset()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }

    private void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Sleep));
    }
    public void Throw(Vector3 startPosition)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0.8f, 1f);
        transform.position = startPosition;
        m_Rigidbody.AddForce(Vector3.up * Random.Range(m_MinSpeed, m_MaxSpeed));
        Invoke(nameof(Sleep), 1.5f);
    }
}
