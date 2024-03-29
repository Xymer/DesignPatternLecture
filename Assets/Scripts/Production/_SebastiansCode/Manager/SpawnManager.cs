﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObjectScriptablePool[] m_Enemies;
    [SerializeField] private MapReaderMono m_MapReader;
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Text m_WinText; 
    List<GameObject> m_ActiveEnemies = new List<GameObject>();
    private int[,] m_EnemyWaveData; // [enemytype, currentwave]
    private int m_CurrentWave = 0;
    int m_EnemiesInCurrentWave = 0;
    private int m_EnemyType = 0;
    private bool m_IsSpawning = false;
    private bool m_CalculateCurrentWave = true;
    private bool m_IsDebugging = false;
    private int m_DisabledEnemies = 0;
    private PathAgent m_PathManager;
    private Player m_Player;
    private Vector3 m_StartPosition;

    private void Awake()
    {
        if (m_Enemies == null)
        {
            Debug.Log($"No scriptablepool");
        }
        m_Player = FindObjectOfType<Player>();
        m_MapReader = GetComponent<MapReaderMono>();
        m_Camera = Camera.main;
    }

    private void Start()
    {
        Initalize();
    }
    private void Update()
    {

    }

    private void Initalize()
    {
        m_MapReader.GenerateMap();
        m_WinText.gameObject.SetActive(false);
        m_EnemyWaveData = m_MapReader.GetEnemyData();
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x, m_Camera.transform.position.y, m_MapReader.GetMapCenter().z);
        m_PathManager = new PathAgent(m_MapReader.GetMapGeneratorPath());
        List<Vector2Int> path = (List<Vector2Int>)m_PathManager.GetPath();
        m_StartPosition = new Vector3(path[0].x, 1, path[0].y);
        SpawnWave();
    }
    [ContextMenu("ChangeMap")]
    private void ChangeMap()
    {
        CancelInvoke(nameof(SpawnEnemy));
        ResetWaveValues();
        m_Player.ResetValues();
        m_WinText.gameObject.SetActive(false);
        m_IsSpawning = false;
       
        m_MapReader.GenerateMap();
        m_EnemyWaveData = m_MapReader.GetEnemyData();
        m_PathManager.ChangePath(m_MapReader.GetMapGeneratorPath());
        List<Vector2Int> path = (List<Vector2Int>)m_PathManager.GetPath();
        while (m_ActiveEnemies.Count != 0)
        {
            m_ActiveEnemies[0].GetComponent<Unit>().SetPath(m_PathManager.GetPath());
            m_ActiveEnemies[0].SetActive(false);
        }
        m_StartPosition = new Vector3(path[0].x, 1, path[0].y);
        m_Camera.transform.position = new Vector3(m_MapReader.GetMapCenter().x, m_Camera.transform.position.y, m_MapReader.GetMapCenter().z);
        SpawnWave();

    }
  
    private void SpawnEnemy()
    {
        if (m_Player.Health.Value <= 0)
        {
            CancelInvoke();
        }
        if (m_CalculateCurrentWave && m_CurrentWave < m_EnemyWaveData.GetLength(1))
        {
            for (int i = 0; i < m_Enemies.Length; i++)
            {
                m_EnemiesInCurrentWave += m_EnemyWaveData[i, m_CurrentWave];
            }
            m_CalculateCurrentWave = false;
        }
        if (m_CurrentWave < m_EnemyWaveData.GetLength(1))
        {
            if (m_EnemiesInCurrentWave == m_DisabledEnemies)
            {
                SetWaveValues();
            }
            else
            {
                if (m_ActiveEnemies.Count + m_DisabledEnemies == m_EnemyWaveData[m_EnemyType, m_CurrentWave])
                {
                    if (m_EnemyType < m_Enemies.Length - 1)
                    {
                        m_EnemyType++;
                    }
                }
                if (m_EnemiesInCurrentWave > m_ActiveEnemies.Count + m_DisabledEnemies)
                {
                    GameObject enemy = m_Enemies[m_EnemyType].Rent(true);
                    enemy.GetComponent<Unit>().SetPath(m_PathManager.GetPath());
                    enemy.transform.position = m_StartPosition;
                    enemy.transform.rotation = Quaternion.identity;
                    EmitOnDisable emitOnDisable = enemy.GetComponent<EmitOnDisable>();
                    emitOnDisable.OnDisableGameObject += EnemyDisabled;

                    m_ActiveEnemies.Add(enemy);
                }
            }
        }
        else
        {
            m_WinText.gameObject.SetActive(true);
            CancelInvoke(nameof(SpawnEnemy));
        }
    }

    private void SetWaveValues()
    {
        m_CurrentWave++;
        m_DisabledEnemies = 0;
        m_EnemyType = 0;
        m_EnemiesInCurrentWave = 0;
        m_CalculateCurrentWave = true;
    }
    private void ResetWaveValues()
    {
        m_CurrentWave = 0;
        m_DisabledEnemies = 0;
        m_EnemyType = 0;
        m_EnemiesInCurrentWave = 0;
        m_CalculateCurrentWave = true;
    }

    private void SpawnWave()
    {
        if (!m_IsSpawning)
        {
            InvokeRepeating(nameof(SpawnEnemy), 1, 1);
            m_IsSpawning = true;
        }
    }
    private void EnemyDisabled(GameObject enemy)
    {
        m_DisabledEnemies++;
        enemy.GetComponent<EmitOnDisable>().OnDisableGameObject -= EnemyDisabled;
        m_ActiveEnemies.Remove(enemy);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_IsDebugging &&m_MapReader != null && m_PathManager.GetPath() != null)
        {
            foreach (Vector2Int item in m_PathManager.GetPath())
            {
                Vector3 draw = new Vector3(item.x, 0, item.y);
                Gizmos.DrawSphere(draw, 1f);
            }
        }
    }
}
