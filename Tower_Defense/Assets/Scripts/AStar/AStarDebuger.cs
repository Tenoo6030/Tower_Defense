using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebuger : MonoBehaviour
{
    //Prefabs References 
    [SerializeField] private GameObject arrowPref;
    [SerializeField] private GameObject debugTilePref;
    
    //TileScript variables
    [SerializeField] private TileScript start, goal;

    //Color
    [SerializeField] private Color32 sColor, gColor, oplColor, clColor, fColor;

    void Start()
    {
       
    }

    
    void Update()
    {
        ClickTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition, goal.GridPosition);

        }
    }
    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider!=null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();
                tmp.IsDebugging = true;

                if (tmp != null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        CreateDebugTile(start.WorldPosition, sColor);
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                        CreateDebugTile(goal.WorldPosition, gColor);
                    }
                }
            }
        }
    }
    public void DebugPath(HashSet<Node> openList, HashSet<Node> closeList, Stack<Node> path)
    {
        foreach (Node node  in openList)
        {
            CreatePath(node,oplColor);
            
        }
        foreach (Node node in closeList)
        {
            if (!path.Contains(node))
            {
                CreatePath(node, clColor);

            }
        }
        foreach(Node node in path)
        {
            CreatePath(node, fColor);
        }
    }
    private void CreatePath(Node node, Color color)
    {
        if (node.TileRef != start && node.TileRef != goal)
        {
            CreateDebugTile(node.TileRef.WorldPosition, color, node);
            PointToParent(node, node.TileRef.WorldPosition);
        }
    }
    private void PointToParent(Node node,Vector2 position)
    {
        if (node.Parent != null)
        {
            GameObject arrow = Instantiate(arrowPref, position,Quaternion.identity);

            Point a = node.GridePosition;
            Point b = node.Parent.GridePosition;

            float angle = Vector2.SignedAngle(new Vector2(b.X - a.X, b.Y - a.Y), new Vector2(-1, 0)) + 180;
            arrow.transform.eulerAngles = new Vector3(0, 0, angle);
            /* switch (a)
             {
                 case Point _ when (a.X < b.X) && (a.Y < b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 315);
                     break;
                 case Point _ when (a.X < b.X) && (a.Y == b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                     break;
                 case Point _ when (a.X < b.X) && (a.Y > b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 45);
                     break;
                 case Point _ when (a.X == b.X) && (a.Y < b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 270);
                     break;
                 case Point _ when (a.X == b.X) && (a.Y > b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 90);
                     break;
                 case Point _ when (a.X > b.X) && (a.Y < b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 225);
                     break;
                 case Point _ when (a.X > b.X) && (a.Y == b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 180);
                     break;
                 case Point _ when (a.X > b.X) && (a.Y > b.Y):
                     arrow.transform.eulerAngles = new Vector3(0, 0, 135);
                     break;

             }*/

        }
        
    }
    private void CreateDebugTile(Vector3 worldPos, Color32 debcolor, Node node = null)
    {   
        GameObject debugTile = Instantiate(debugTilePref, worldPos, Quaternion.identity);
        if (node != null)
        {
            DebugTile dT = debugTile.GetComponent<DebugTile>();
            dT.G.text += node.G;
            dT.H.text += node.H;
            dT.F.text += node.F;

        }
        debugTile.GetComponent<SpriteRenderer>().color = debcolor;
    }

}
