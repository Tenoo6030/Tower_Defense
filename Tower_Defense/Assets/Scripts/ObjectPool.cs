using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPref;

    public GameObject GetObject(string type)
    {
        for (int i = 0; i < enemyPref.Length; i++)
        {
            if (enemyPref[i].name == type)
            {
                GameObject newObject = Instantiate(enemyPref[i]);
                newObject.name = type;
                return newObject;
            }
        }

        return null;
    }
}
