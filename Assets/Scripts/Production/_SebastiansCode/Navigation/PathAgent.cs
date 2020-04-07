using System;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent
{
    IEnumerable<GameObject> m_Agents;
    IEnumerable<Vector2Int> m_Path;

    public PathAgent(IEnumerable<GameObject> agents, IEnumerable<Vector2Int> path)
    {
        m_Agents = agents;
        m_Path = path;
    }

    public IEnumerable<Vector2Int> GetPath()
    {
        return m_Path;
    }

    public void MoveAgent()
    {

    }
}

