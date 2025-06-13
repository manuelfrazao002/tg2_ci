using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodGameManager : MonoBehaviour
{
    public GameObject[] foodObjects;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    private int currentIndex = 0;
    private int correctCount = 0;
    private int wrongCount = 0;

    public string levelKey = "Level1Completed";
    public string sceneToLoad = "Jogo1";

    public bool quizFinished { get; private set; } = false;

    void Start()
    {
        ShowFood();
    }

    public void Answer(bool userChoice, bool isActuallyHealthy)
    {
        if (userChoice == isActuallyHealthy)
            correctCount++;
        else
            wrongCount++;

        currentIndex++;
        ShowFood();
    }

    void ShowFood()
    {
        foreach (var obj in foodObjects)
            obj.SetActive(false);

        if (currentIndex < foodObjects.Length)
        {
            foodObjects[currentIndex].SetActive(true);
        }
        else
        {
            quizFinished = true;
            correctText.text = "Certo: " + correctCount;
            wrongText.text = "Errado: " + wrongCount;

            correctText.gameObject.SetActive(true);
            wrongText.gameObject.SetActive(true);

            // Só completa o nível se todas as respostas estiverem corretas
            if (wrongCount == 0 && correctCount == foodObjects.Length)
            {
                PlayerPrefs.SetInt(levelKey, 1); // salva que o nível foi completado
                PlayerPrefs.Save();
            }
            else
            {
                // Garante que o nível não está marcado como completo
                PlayerPrefs.SetInt(levelKey, 0);
                PlayerPrefs.Save();
            }

            Invoke(nameof(CompleteLevel), 3f); // Espera 3 segundos e volta ao menu
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene(sceneToLoad); // volta para seleção de níveis
    }
}