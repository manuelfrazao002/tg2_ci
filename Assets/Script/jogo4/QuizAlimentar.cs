using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class QuizAlimentar : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] imageButtons;
    public int[] ButtonsValue;
    public Sprite spriteCorreto;
    public Sprite spriteErrado;
    private bool showHealthyQuestion = false;
    private Sprite[] botoesSpritesOriginais;
    private System.Action pendingCallback;

    void Start()
    {
        questionText.text = "Qual destes não é um hábito saudável?";
        AssignButtonListeners();

        botoesSpritesOriginais = new Sprite[imageButtons.Length];
        for (int i = 0; i < imageButtons.Length; i++) {
            botoesSpritesOriginais[i] = imageButtons[i].GetComponent<Image>().sprite;
        }
    }

    void AssignButtonListeners()
    {
        for (int i = 0; i < imageButtons.Length; i++)
        {
            int index = i;
            imageButtons[i].onClick.AddListener(() => OnImageSelected(index));
        }
    }

    void OnImageSelected(int selectedIndex)
    {
        int value = ButtonsValue[selectedIndex];
        Button clickedButton = imageButtons[selectedIndex];
        Image buttonImage = clickedButton.GetComponent<Image>();

        bool respostaCorreta;

        if (!showHealthyQuestion)
        {
            respostaCorreta = (value == 2);
            if (respostaCorreta)
            {
                MostrarFeedbackEAvancar(buttonImage, clickedButton, true, MudarParaPerguntaSaudavel);
            }
            else
            {
                MostrarFeedbackErrado(buttonImage, clickedButton);
            }
        }
        else
        {
            respostaCorreta = (value == 1);
            if (respostaCorreta)
            {
                MostrarFeedbackEAvancar(buttonImage, clickedButton, true, IrParaCenaFinal);
            }
            else
            {
                MostrarFeedbackErrado(buttonImage, clickedButton);
            }
        }

    }

    void MostrarFeedbackEAvancar(Image img, Button btn, bool correto, System.Action callback) {
        img.sprite = spriteCorreto;
        btn.interactable = false;
        Invoke(nameof(ExecutarCallback), 1.5f);
        pendingCallback = callback;
    }

    void MostrarFeedbackErrado(Image img, Button btn) {
        img.sprite = spriteErrado;
        btn.interactable = false;
    }

    void ExecutarCallback() {
        pendingCallback?.Invoke();
        pendingCallback = null;
    }

    void MudarParaPerguntaSaudavel() {
        showHealthyQuestion = true;
        questionText.text = " Qual destes é um hábito saudável?";
        ResetarBotoes();
    }

    void IrParaCenaFinal()
    {
        SceneManager.LoadScene("Aprender4-2");
    }

    void ResetarBotoes() {
        for (int i = 0; i < imageButtons.Length; i++) {
            imageButtons[i].interactable = true;
            imageButtons[i].GetComponent<Image>().sprite = botoesSpritesOriginais[i];
        }
    }
}
