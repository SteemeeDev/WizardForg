using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

    [SerializeField] GameObject[] enemyArray1;
    [SerializeField] GameObject[] enemyArray2;

    private void Start()
    {
        foreach (GameObject go in enemyArray1)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in enemyArray2)
        {
            go.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject go in enemyArray1)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in enemyArray2)
        {
            go.SetActive(true);
        }
    }

   

}
