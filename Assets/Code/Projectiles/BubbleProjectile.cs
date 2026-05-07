using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProjectile : Projectile
{
    [SerializeField] Animator _animator;
    public float chargeUpTime = 2f;
    public float charge = 0f;

    bool hitEnvironment = false;
    bool fired = false;

    // Debounce variable that triggers the onShoot
    bool onFired = false;

    ChargeUpBar _chargeUpBar;
    BubbleWand _wandController;

    List<GameObject> hitEnemies = new List<GameObject>();

    Vector3 travelDir = Vector3.zero;
 
    public IEnumerator FireBubble(WandController controller, Transform startPos, ChargeUpBar chargeUpBar)
    {
        controller.StopAllCoroutines();
        controller.StartCoroutine(controller.IEFadeAudio(2f, 1f, false));
        _wandController = controller.gameObject.GetComponent<BubbleWand>();
        _chargeUpBar = chargeUpBar;
        while (timeAlive < lifetime)
        {
            if (hitEnvironment)
            {
                controller.StopAllCoroutines();
                controller.StartCoroutine(controller.IEFadeAudio(.1f, 0f, false));
                yield break;
            }
            timeAlive += Time.deltaTime;

            if (Input.GetMouseButton(0) 
                && !fired
                && charge <= chargeUpTime 
                && controller != null 
                && controller.gameObject.activeSelf)
            {
                transform.position = new Vector3(
                    startPos.position.x,
                    PlayerController.Instance.transform.position.y,
                    startPos.position.z
                )
                + Quaternion.Euler(0,45,0) * new Vector3(
                    0,
                    0,
                    -controller.wandToPlayer.z * 2
                );

                charge += Time.deltaTime;
                transform.localScale = Vector3.one * charge / chargeUpTime;

                chargeUpBar.UpdateBar((charge / chargeUpTime) * chargeUpBar.maxCharge);

                var shape = _wandController.bubbleParticles.shape;
                shape.radius = (charge / chargeUpTime) * 0.5f;
                var bubbleEmission = _wandController.bubbleParticles.emission;
                bubbleEmission.rateOverTimeMultiplier = Mathf.Pow(charge / chargeUpTime, 2) * 40;

                travelDir = Quaternion.Euler(0,45,0) * -new Vector3(controller.playerLook.x, 0, controller.playerLook.y);
                travelDir = travelDir.normalized;

                Debug.DrawRay(transform.position, travelDir, Color.magenta);
            }
            else if (fired && !onFired)
            {
                onFired = true;
                OnFireWand();
            }
            else
            {
                fired = true;
                _rigidBody.velocity = new Vector3(travelDir.x, transform.position.y, travelDir.z) * travelSpeed;
                Debug.DrawRay(transform.position, travelDir, Color.yellow);
            }

            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }

    // We do an extra check for enemys when firing since, if the bubble is created inside an enemy, it wont trigger the OnTriggerEnter and thus wont deal damage
    void OnFireWand()
    {
        if (charge < 0.2f) Destroy(gameObject);
        _wandController.StopAllCoroutines();
        _wandController.StartCoroutine(_wandController.IEFadeAudio(0.1f, 0f, false));
        _chargeUpBar.UpdateBar(0f);
        var bubbleEmission = _wandController.bubbleParticles.emission;
        bubbleEmission.rateOverTimeMultiplier = 0;
        // Debug.Log("Fired bubble awnd");
        Collider[] hits = Physics.OverlapSphere(transform.position, transform.lossyScale.magnitude, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
               // Debug.Log("Hit enemy");
                hit.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.Pow(charge / chargeUpTime, 2f) * 50f);

                charge -= 0.5f;
                transform.localScale = Vector3.one * charge / chargeUpTime;

                travelSpeed *= 0.8f;
            }
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && fired)
        {
            if (hitEnemies.Contains(collision.gameObject)) return;
            else hitEnemies.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.Pow(charge / chargeUpTime, 2f) * 50f);

            charge -= 0.5f;
            if (charge <= 0.1f) Destroy(gameObject);

            transform.localScale = Vector3.one * charge / chargeUpTime;
            travelSpeed *= 0.8f;

            _wandController.bubblePop.pitch = Random.Range(0.9f, 1.1f);
            _wandController.bubblePop.PlayOneShot(_wandController.bubblePop.clip);

        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.isKinematic = true;
            _animator.SetTrigger("KillBubble");

            hitEnvironment = true;
            var bubbleEmission = _wandController.bubbleParticles.emission;
            bubbleEmission.rateOverTimeMultiplier = 0;
            _wandController.StartCoroutine(_wandController.IEFadeAudio(0.1f, 0f, false));


            StartCoroutine(IEDestroyObject());
        }
    }

    // Called by animationevent on _animator
    public IEnumerator IEDestroyObject()
    {
        _wandController.bubblePop.Play();
        yield return new WaitForSeconds(2f);
        _animator.speed = 0;
        Destroy(gameObject);
    }
}