using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }
    public Vector2 WorldPosition
    {
        get
        {
            Vector2 customPivot = LevelManager.Instance.tileSize/2;
            return new Vector2(transform.position.x + customPivot.x, transform.position.y - customPivot.y);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 wordPos,Transform parent)
    {
        GridPosition = gridPos;
        transform.position = wordPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos,this);
    }
}
