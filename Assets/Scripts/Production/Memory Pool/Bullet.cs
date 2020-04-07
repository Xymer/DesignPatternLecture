using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody rigidbody;

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
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
        rigidbody.AddForce(Vector3.up * Random.Range(minSpeed, maxSpeed));
        Invoke(nameof(Sleep), 1.5f);
    }
}
