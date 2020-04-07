using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SingletonConfiguration("sgl_EnemyManager")]
public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private GameObject m_EnemyPrefab;
   

    public void CreateEnemy()
    {
       Instantiate(m_EnemyPrefab);
    }
}
