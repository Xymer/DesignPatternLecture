using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCaller : MonoBehaviour
{
    [ContextMenu("Create Enemy")]
    public void CreateEnemy()
    {
        EnemyManager.Instance.CreateEnemy();
    }
    [ContextMenu("Call POCO singleton")]
    public void CallPOCO()
    {
      string fileName = FileManager.Instance.GetFileName();
        Debug.Log(fileName);
    }
   [ContextMenu("Load Json data")]
   public void GetJsonData()
    {
        string data = ResourceManager.Instance.GetJsonData();
        Debug.Log(data);
    }
}
