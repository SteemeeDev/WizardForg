using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;
    public static CameraShake Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(IEShakeCamera(.3f, 0.4f));
        }
    }
    public IEnumerator IEShakeCamera(float duration, float intensity)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = transform.localPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * intensity;
            yield return null;
        }
    }
}
