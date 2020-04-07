using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletComponentPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObjectScriptablePool scriptablePool;

    private GameObjectPool bulletPool;
    private ComponentPool<Bullet> bulletComponentPool;
    private void Awake()
    {
        //bulletPool = new GameObjectPool(10, bulletPrefab, 1, new GameObject("Bullet Parent").transform);
        //bulletComponentPool = new ComponentPool<Bullet>(1,bulletComponentPrefab, 1, new GameObject("Bullet Component Parent").transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            //GameObject bullet = bulletPool.Rent(false);
            //Bullet bulletComponent = bulletComponentPool.Rent(false);
            //bulletComponent.GetComponent<Bullet>();
            //bulletComponent.transform.position = transform.position;
            //bulletComponent.Reset();
            //bulletComponent.GetComponent<Renderer>().material.color = Random.ColorHSV(0.8f, 1f);
            //bulletComponent.gameObject.SetActive(true);
            //bulletComponent.Push();

            GameObject bullet = scriptablePool.Rent(true);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.Throw(transform.position);
        }
    }
    private void OnDestroy()
    {
        //bulletPool.Dispose();
        //bulletComponentPool.Dispose();
    }
}
