using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar 
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }
    public static Stack<Node> GetPath(Point start, Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closeList = new HashSet<Node>();
        Stack<Node> finalPath = new Stack<Node>();
        Node currentNode = nodes[start];
        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.GridePosition.X - x, currentNode.GridePosition.Y - y);
                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].IsWalkable && neighbourPos != currentNode.GridePosition)
                    {
                        int gCost;
                        if (Mathf.Abs(x - y)==1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        Node neighbour = nodes[neighbourPos];

                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValus(currentNode, nodes[goal], gCost);
                            }
                            
                        }

                        else if (!closeList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValus(currentNode, nodes[goal], gCost);
                        }

                    }

                }
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }
            
            if (currentNode == nodes[goal])
            {
                while(currentNode.GridePosition!= start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                
                break;
            }
            
        }

        return finalPath;
        //Onli for Debugging
        //GameObject.Find(" AStarDebuger").GetComponent<AStarDebuger>().DebugPath(openList,closeList,finalPath);
    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = neighbour.GridePosition - currentNode.GridePosition;

        Point first = new Point(currentNode.GridePosition.X + direction.X, currentNode.GridePosition.Y);
        Point second = new Point(currentNode.GridePosition.X, currentNode.GridePosition.Y + direction.Y);

         if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].IsWalkable)
         {
             return false;
         }
         if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].IsWalkable)
         {
             return false;
         }

         return true;

    }
}
