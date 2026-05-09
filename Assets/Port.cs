using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField] Animator _animatior;
    [SerializeField] PlayerTeleport playerTeleport;
    [SerializeField] enemySpawn enemySpawner;
    PlayerController playerController;
    private void Start()
    {
        playerController = PlayerController.Instance;
    }

    bool isClosed;
    private void Update()
    {

        float distFromPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (distFromPlayer < 4f && isClosed)
        {
            if (playerTeleport != null && !playerTeleport.hasTped)
            {
                isClosed = false;
                _animatior.SetTrigger("OpenPort");
            }
            if (enemySpawner != null && enemySpawner.spawnedAllEnemies && enemySpawner.enemyList.Count == 0)
            {
                isClosed = false;
                _animatior.SetTrigger("OpenPort");
            }
        }
        else if (!isClosed && distFromPlayer >= 4f)
        {
            isClosed = true;
            _animatior.SetTrigger("ClosePort");
        }
    }
}
