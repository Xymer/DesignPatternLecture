using System;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent
{

    List<ScriptableEnemies> m_Agents;
    IEnumerable<Vector2Int> m_Path;

    public PathAgent(List<ScriptableEnemies> agents, IEnumerable<Vector2Int> path)
    {
        m_Agents = agents;
        foreach (ScriptableEnemies agent in agents)
        {
            agent.Path = (List<Vector2Int>)path;
        }
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
            agent.Move();
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

