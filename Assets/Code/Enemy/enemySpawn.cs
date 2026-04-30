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
        // I love looping through arrays :D
        for (int i = 0; i < enemyArray1.Length; i++)
        {
            if (enemyArray1[i] == null)
            {
                for(int j = 0;  j < enemyArray2.Length; j++)
                {
                    if (enemyArray2[j] == null)
                    {
                        winScreen.SetActive(true);
                        Time.timeScale = 0;

                        
                    }
                }

            }
        }
        
    }


}
