using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Projectile : MonoBehaviour
{
    public Rigidbody _rigidBody;
    public float lifetime = 5f;
    public float timeAlive = 0f;
    public float travelSpeed;
    public virtual IEnumerator FireProjectile(WandController controller, Transform startPos)
    {
        yield return null;
    }
}
