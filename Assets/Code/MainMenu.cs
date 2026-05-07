using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] AudioSource menuMusic;
    public void PlayGame()
    {
        StartCoroutine(IEPlayGame());
    }

    IEnumerator IEPlayGame()
    {
        float elapsed = 0;
        float animTime = 2f;

        while (elapsed < animTime)
        {
            elapsed += Time.deltaTime;
            blackScreen.color = new Color(0, 0, 0, elapsed/animTime);
            menuMusic.volume = 1f-elapsed/animTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
