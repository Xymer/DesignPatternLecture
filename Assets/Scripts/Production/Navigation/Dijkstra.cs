using System;
using UnityEngine;
using System.Collections.Generic;
using Tools;

namespace AI
{
    public class Dijkstra : IPathFinder
    {
        private List<Vector2Int> validNodes;      
        
        public Dijkstra(List<Vector2Int> newGrid)
        {
            validNodes = newGrid;
        }

        public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            Vector2Int currentNode = start;
            Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>();
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(currentNode);

            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                if (currentNode == goal)
                {
                    break;
                }
                foreach (Vector2Int dir in DirectionTools.Dirs)
                {
                    Vector2Int node = currentNode + dir;
                    if (validNodes.Contains(node))
                    {
                        if (!ancestors.ContainsKey(node))
                        {
                            queue.Enqueue(node);
                            ancestors.Add(node, currentNode);
                        }
                    }
                }
                
            }

            if (ancestors.ContainsKey(goal))
            {
                List<Vector2Int> path = new List<Vector2Int>();
                foreach (var node in ancestors)
                {
                    path.Add(node.Key);
                }
                path.Reverse();
                return path;
            }

            return null;
        }
    }
}

