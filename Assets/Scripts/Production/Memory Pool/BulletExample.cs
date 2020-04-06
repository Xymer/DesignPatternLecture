using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExample : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody rigidbody;
    private void Push()
    {
        rigidbody.AddForce(Vector3.up * Random.Range(minSpeed,maxSpeed));
    }
}
