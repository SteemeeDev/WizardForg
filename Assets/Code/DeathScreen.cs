using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    public void retry()
    {
        SceneManager.LoadScene("Brugertest");
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
