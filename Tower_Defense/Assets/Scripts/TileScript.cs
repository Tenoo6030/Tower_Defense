using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TileScript : MonoBehaviour
{
    [SerializeField] private Color fullColor = new Color32(255, 118, 118, 255);
    [SerializeField] private Color emptyColor = new Color32(118, 255, 118, 255);
    private SpriteRenderer tileSR;
    public bool IsDebugging { get; set; }
    public bool IsWalkable { get; set; }
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
        tileSR = GetComponent<SpriteRenderer>();

    }

    public void Setup(Point gridPos, Vector3 wordPos,Transform parent)
    {
        /* if (this.CompareTag("Obstacle"))
         {
             IsWalkable = false;
         }
         else
         {
             IsWalkable = true;

         }*/
        IsWalkable = true;
        GridPosition = gridPos;
        transform.position = wordPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos,this);

    }

    private void OnMouseOver()
    {

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null)
        {

            if ((transform.childCount == 0) && !IsDebugging) { ColorTile(emptyColor); }

            if ((transform.childCount != 0) && !IsDebugging) { ColorTile(fullColor); }
            
            else if (Input.GetMouseButtonDown(0)) { PlaceTower(); }

        }

    }

    private void OnMouseExit()
    {
        if (!IsDebugging) { ColorTile(Color.white);}

    }

    private void PlaceTower()
    {
        GameObject tower = Instantiate(GameManager.Instance.ClickedButton.TowerPref, transform.position, Quaternion.identity);
        tower.transform.SetParent(transform);
        tileSR.sprite = LevelManager.Instance.TowerBacgrand;
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        Hover.Instance.Disactivate();
        GameManager.Instance.BayTower();
        IsWalkable = false;

    }

    private void ColorTile(Color newColor)
    {
        tileSR.color = newColor;

    }
}