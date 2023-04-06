using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Point GridePosition { get; private set; }
    public TileScript TileRef { get; private set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F { get; set; }

    public Node Parent { get; private set; }

    public Node(TileScript tileRef)
    {
        TileRef = tileRef;
        GridePosition = tileRef.GridPosition;
    }
    public void CalcValus(Node parent,Node goal, int gCost)
    {
        Parent = parent;
        G = parent.G + gCost;
        H = (Mathf.Abs(GridePosition.X - goal.GridePosition.X) + Mathf.Abs(GridePosition.Y - goal.GridePosition.Y)) * 10;
        F = G + H;
    }
}
