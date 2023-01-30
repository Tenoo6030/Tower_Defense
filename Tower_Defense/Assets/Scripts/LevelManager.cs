using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclesTilePref;
    private GameObject[] roadTilePref;

    public float ObstaclesTileSize
    {
        get { return obstaclesTilePref[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;}
    }
    /* public float RoadTileSize
    {
        get
        {
            return roadTilePref[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        string[] mapData=ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTile = mapData[y].ToCharArray();

            for (int x = 0; x< mapX; x++)
            {
                PLaceTile(newTile[x].ToString(),x,y,worldStart);
            }
        }
    }

    private void PLaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(obstaclesTilePref[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x +(ObstaclesTileSize * x),worldStart.y - (ObstaclesTileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }
}
