using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float timeLeft = 30f;
    public TextMeshProUGUI timerText;
    public GameObject losePanel;
    public string sceneToLoad = "MainMenu";

    private bool isGameOver = false;
    private FoodGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<FoodGameManager>();
        UpdateTimerDisplay();
        if (losePanel != null) losePanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver || gameManager.quizFinished) return; // <-- NOVO: para se o quiz acabar

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            GameOver();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        timerText.text = "Tempo: " + Mathf.CeilToInt(timeLeft).ToString();
    }

    void GameOver()
    {
        isGameOver = true;

        if (losePanel != null)
            losePanel.SetActive(true);

        Invoke("LoadScene", 1.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
