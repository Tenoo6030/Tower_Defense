using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePref;
    [SerializeField] private CameraMovemnt cam;

    public Dictionary<Point,TileScript> Tiles { get; set; }
    public float TileSize
    {
        get { return tilePref[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;}
    }
  
    void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        string[] mapData=ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

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
    }

    private void PLaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        TileScript newTile = Instantiate(tilePref[tileIndex]).GetComponent<TileScript>();
        newTile.Setup(new Point(x, y),new Vector3(worldStart.x +(TileSize * x),worldStart.y - (TileSize * y), 0));
        Tiles.Add(new Point(x, y), newTile);
       
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
       
        return data.Split('-');
    }
}
