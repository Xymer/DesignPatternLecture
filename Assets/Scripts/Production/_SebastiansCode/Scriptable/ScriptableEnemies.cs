using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/Enemies/Enemy"))]
public class ScriptableEnemies : ScriptableObject
{
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private int m_Health;

    [SerializeField] private GameObject m_Prefab;

}
