﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject greenPortalPref, purplePortalPref;
    [SerializeField] private GameObject[] tilePref;
    [SerializeField] private CameraMovemnt cam;
    [SerializeField] private Transform map;

    private Point mapSize;
    private Point greenSpawn, purpleSpawn;
    private Stack<Node> path;

    [HideInInspector] public Vector2 tileSize;
    [HideInInspector] public float TileSize => tileSize.x;
    [HideInInspector] public Sprite TowerBacgrand => tilePref[0].GetComponent<SpriteRenderer>().sprite;

    public Dictionary<Point,TileScript> Tiles { get; set; }

    public Portal GreenPortal { get; set; }

    public Stack<Node> Path { 
        get 
        {
            if (path==null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }
    
    void Start()
    {
        tileSize = tilePref[0].GetComponent<SpriteRenderer>().sprite.bounds.size;
        CreateLevel();
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        string[] mapData=ReadLevelText();
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;
        mapSize = new Point(mapX,mapY);

        Vector3 maxTile;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTile = mapData[y].ToCharArray();

            for (int x = 0; x< mapX; x++)
            {
               PLaceTile(newTile[x].ToString(),x,y,worldStart);
            }
        }
        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        cam.SetLimites(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
        SpawnPortals();
    }

    private void PLaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePref[tileIndex]).GetComponent<TileScript>();
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0),map);
       
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
       
        return data.Split('-');
    }

    private void SpawnPortals()
    {
        greenSpawn = new Point(0, 0);
        GameObject tmp = Instantiate(greenPortalPref, Tiles[greenSpawn].WorldPosition,Quaternion.identity);
        GreenPortal = tmp.GetComponent<Portal>();
        GreenPortal.name = "GreenPortal";

        purpleSpawn = new Point(9, 4);
        Instantiate(purplePortalPref, Tiles[purpleSpawn].WorldPosition,Quaternion.identity);

    }

    public bool InBounds(Point position) => position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
  
    public void GeneratePath()
    {
        path = AStar.GetPath(greenSpawn, purpleSpawn);
    }
}
