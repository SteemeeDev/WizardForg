
using UnityEngine;


public class LazerWand : WandController
{
    [SerializeField] LineRenderer lazerRenderer;
    [SerializeField] float maxLazerLength = 20f;
    [SerializeField] Transform firePoint2;
    public float damagePerTick = 5f;
    public int ticksPerSecond = 10;

    float timeSinceLastTick = 0f;

    EnemyHealth targetedEnemy;

    int layerMask;

    private void Start()
    { 
        layerMask = (1 << LayerMask.NameToLayer("3dEnvironment")) | (1 << LayerMask.NameToLayer("Enemy"));
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0))
        {
            lazerRenderer.enabled = false;
            targetedEnemy = null;
            firePoint2.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            FireWand();
        }

        timeSinceLastTick += Time.deltaTime;
        if (timeSinceLastTick >= 1f / ticksPerSecond)
        {
            timeSinceLastTick = 0f;
            if (targetedEnemy != null)
            {
                targetedEnemy.TakeDamage(damagePerTick);
            }
        }
    }

    public override void FireWand()
    {
        lazerRenderer.enabled = true;
        lazerRenderer.SetPosition(0, firePos.position);

        if (Physics.BoxCast(
            center: firePos.position - firePos.up * 2,
            halfExtents: new Vector3(0.01f, 0.01f, 20f),
            direction: firePos.up,
            out RaycastHit hit,
            orientation: firePos.rotation,
            maxDistance: maxLazerLength,
            layerMask: layerMask
        ))
        {
            //Debug.Log("Lazer Hit: " + hit.transform.name + " With tag " + hit.transform.gameObject.tag);

            if (Mathf.Sign(Vector3.Dot(firePos.up, (hit.point - firePos.position))) == -1f)
            {
                lazerRenderer.enabled = false;
                firePoint2.gameObject.SetActive(false);
            }
            else
            {
                lazerRenderer.enabled = true;
                firePoint2.gameObject.SetActive(true);
            }

            float distance = (hit.point - firePos.position).magnitude;
            float angle = Mathf.Asin(Vector3.Dot(firePos.up, (hit.point - firePos.position).normalized));

            Vector3 firepos2 =
                firePos.position 
                + (firePos.up * distance) * angle * 0.85f
                - firePos.up * 0.1f
            ;

           // Debug.Log(angle);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                if (targetedEnemy == null || targetedEnemy.transform != hit.transform) targetedEnemy = hit.transform.gameObject.GetComponent<EnemyHealth>();
                firepos2 += firePos.up * 0.2f;
                firePoint2.gameObject.SetActive(true);
            }
            else
            {
                targetedEnemy = null;
            }

            firePoint2.position = firepos2;
            lazerRenderer.SetPosition(1, firepos2);
        }
        else
        {
            targetedEnemy = null;
            Vector3 firepos2 = firePos.position + (firePos.up * maxLazerLength);
            firePoint2.position = firepos2;
            lazerRenderer.SetPosition(1, firepos2);
        }

    }
}
