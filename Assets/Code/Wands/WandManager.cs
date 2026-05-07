using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class WandManager : MonoBehaviour
{
    // These 2 lists need to be in the same order as the "Wand" enum
    public WandController[] wands;
    [SerializeField] Animator[] UIWandAnimators;

    [SerializeField] int currentWandIndex;
    public WandController currentWand;
    int previousWandIndex;

    public Coroutine lazerOvercharge;

    AudioSource _audioSource;

    public enum Wand
    {
        BubbleWand,
        LazerWand,
        StarWand
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SwitchWand();
    }
    private void Update()
    {
        // Yandere dev ahh code :sob:
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            previousWandIndex = currentWandIndex;
            currentWandIndex = (int)Wand.BubbleWand;
            SwitchWand();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            previousWandIndex = currentWandIndex;
            currentWandIndex = (int)Wand.LazerWand;
            SwitchWand();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            previousWandIndex = currentWandIndex;
            currentWandIndex = (int)Wand.StarWand;
            SwitchWand();
        }


        wands[(int)Wand.StarWand].GetComponent<StarWand>().timeSinceLastShot += Time.deltaTime;

        wands[(int)Wand.StarWand].chargeUpBar.maxCharge = wands[(int)Wand.StarWand].GetComponent<StarWand>().cooldown;
        wands[(int)Wand.StarWand].chargeUpBar.UpdateBar(wands[(int)Wand.StarWand].GetComponent<StarWand>().timeSinceLastShot);
    }

    void SwitchWand()
    {
        _audioSource.Play();
        if (currentWand != null)
        {
            currentWand.gameObject.SetActive(false);
            UIWandAnimators[previousWandIndex].SetBool("Selected", false);
            UIWandAnimators[previousWandIndex].transform.parent.transform.localScale = Vector3.one;
        }
        currentWand = wands[currentWandIndex];
        currentWand.gameObject.SetActive(true);
        UIWandAnimators[currentWandIndex].SetBool("Selected", true);
        UIWandAnimators[currentWandIndex].transform.parent.transform.localScale = Vector3.one * 1.2f;
    }

    public IEnumerator IEOverchargeLazer(float elapsed)
    {
        LazerWand lazer = wands[(int)Wand.LazerWand].gameObject.GetComponent<LazerWand>();
        lazer.overCharged = true;

        if (elapsed <= 0) elapsed = lazer.maxLazerCharge;

        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime * lazer.chargeDownTime * 0.35f;
            lazer.lazerCharge = elapsed;
            lazer.chargeUpBar.UpdateBar(lazer.lazerCharge);
            yield return null;
        }
        lazer.overCharged = false;
    }
}
