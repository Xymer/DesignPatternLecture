using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SingletonConfiguration("sgl_EnemyManager")]
public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private GameObject enemyPrefab;
    // Start is called before the first frame update

    public void CreateEnemy()
    {
       Instantiate(enemyPrefab);
    }
}
