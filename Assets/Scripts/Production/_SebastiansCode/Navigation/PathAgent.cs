using System;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent
{
    List<ScriptableEnemies> m_Units = new List<ScriptableEnemies>();
    List<ScriptableEnemies> m_Agents = new List<ScriptableEnemies>();
    IEnumerable<Vector2Int> m_Path;

    public PathAgent(List<ScriptableEnemies> units, IEnumerable<Vector2Int> path)
    {
        m_Units = units;
        m_Path = path;
    }

    public IEnumerable<Vector2Int> GetPath()
    {
        return m_Path;
    }
    /// <summary>
    /// Gets a specific index in the path
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector2Int GetPath(int index)
    {
        List<Vector2Int> path = (List<Vector2Int>)m_Path;
        return path[index];
    }

    public void MoveAgent()
    {
        foreach (ScriptableEnemies agent in m_Agents)
        {
            agent.CurrentPath++;
        }
    }
    public void ChangePath(IEnumerable<Vector2Int> newPath)
    {
        m_Path = newPath;
    }
    public void AddUnitToAgents(ScriptableEnemies unit)
    {
        m_Agents.Add(unit);
    }
}

