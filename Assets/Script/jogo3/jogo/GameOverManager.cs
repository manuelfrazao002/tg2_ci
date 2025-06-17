using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class GameOverManager : MonoBehaviour
{
    public Text timerText;
    public Text gameOverText;
    public GameObject gameOverUI;
    private Coroutine blinkCoroutine;
    public string sceneToLoad = "Nivel3-5";

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            timerText.enabled = true;
        }

        gameOverText.text = "O tempo acabou!";

        Time.timeScale = 1f;
        StartCoroutine(WaitAndLoadScene(3f));
    }

    IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
