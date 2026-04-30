using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{

    [SerializeField] GameObject[] enemyArray1;
    [SerializeField] GameObject[] enemyArray2;

    [SerializeField] GameObject winScreen;

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

    private void FixedUpdate()
    {
        WinScreen();
    }

    private void WinScreen()
    {
        bool isArray1Empty = true;
        bool isArray2Empty = true;

        // I love looping through arrays :D
        for (int i = 0; i < enemyArray1.Length; i++)
        {
            if (enemyArray1[i] != null)
            {
                isArray1Empty = false;
                break;
            }
        }
        
        for (int i = 0;i < enemyArray2.Length;i++)
        {
            if (enemyArray2[i] != null)
            {
                isArray2Empty = false;
                break;
            }
                     
        }
        
        if (isArray1Empty && isArray2Empty)
        {
            winScreen.SetActive(true);
        }

    }


}
