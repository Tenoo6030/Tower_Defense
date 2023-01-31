using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemnt : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private float xMax;
    private float yMin;
   
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        float actualSpeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * actualSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * actualSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * actualSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * actualSpeed);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin,0), -10);

    }
    public void SetLimites(Vector3 maxTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
    }
}
