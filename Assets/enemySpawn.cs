using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

    [SerializeField] GameObject[] enemyArray;

    private void Start()
    {
        foreach (GameObject go in enemyArray)
        {
            go.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject go in enemyArray)
        {
            go.SetActive(true);
        }
    }

}
