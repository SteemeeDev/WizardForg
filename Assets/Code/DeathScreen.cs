using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
   public void retry()
    {
        SceneManager.LoadScene("Brugertest");
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
