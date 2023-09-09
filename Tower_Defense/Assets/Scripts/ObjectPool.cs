using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPref;
    private List<GameObject> enemyList = new List<GameObject>(); 

    public GameObject GetObject(string type)
    {
        foreach (GameObject enemy in enemyList)
        {
            if (enemy.name == type && !enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        for (int i = 0; i < enemyPref.Length; i++)
        {
            if (enemyPref[i].name == type)
                {
                GameObject newObject = Instantiate(enemyPref[i]);
                enemyList.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }
       
        return null;

    }

    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
