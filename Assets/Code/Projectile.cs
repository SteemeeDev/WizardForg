using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;

    public Vector3 travelDir = Vector3.zero;
    public float lifetime = 5f;

    float timeAlive = 0f;
    private void FixedUpdate()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifetime) Destroy(gameObject);
        
        _rigidBody.velocity = travelDir;
    }
}
