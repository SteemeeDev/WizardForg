using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WandManager : MonoBehaviour
{
    // These 2 lists need to be in the same order as the "Wand" enum
    [SerializeField] WandController[] wands;
    [SerializeField] Animator[] UIWandAnimators;

    [SerializeField] int currentWandIndex;
    public WandController currentWand;
    int previousWandIndex;

    public Coroutine lazerOvercharge;

    public enum Wand
    {
        BubbleWand,
        LazerWand,
        StarWand
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
    }

    void SwitchWand()
    {
        if (currentWand != null)
        {
            currentWand.gameObject.SetActive(false);
            UIWandAnimators[previousWandIndex].SetBool("Selected", false);
        }
        currentWand = wands[currentWandIndex];
        currentWand.gameObject.SetActive(true);
        UIWandAnimators[currentWandIndex].SetBool("Selected", true);
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
