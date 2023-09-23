using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private float speead;
    
    private Stack<Node> path;
    private Vector3 destination;

    public Point GridePosition { get; set; }
    public bool IsActive { get; set; }

    private void Update()
    {
        if (IsActive)
        {
             Move();
        }
       
    }
    public void Spawn()
    {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1f, 1f), false));
        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from,Vector3 to, bool remove)
    {
       
        float progres = 0;

        while (progres <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progres);
            progres += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;
        if (remove)
        {
            Release();
        }
    }
    
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speead*Time.deltaTime);
        if (transform.position == destination)
        {
            if (path != null && path.Count > 0)
            {
                GridePosition = path.Peek().GridePosition;
                destination = path.Pop().WorldPosition;
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;
            GridePosition = path.Peek().GridePosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PurplePortal"))
        {
            StartCoroutine(Scale(new Vector3(1f, 1f), new Vector3(0.1f, 0.1f), true));
        }
    }

   private void Release()
    {
        IsActive = false;
        GridePosition = LevelManager.Instance.GreenSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveEnemy(this);
    }
}
