using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tempoLimite = 30f; // tempo total em segundos
    public TMP_Text textoTemporizador; // referência ao Text da UI

    private float tempoRestante;
    private bool jogoAtivo = true;

    void Start()
    {
        tempoRestante = tempoLimite;
    }

    void Update()
    {
        if (!jogoAtivo) return;

        tempoRestante -= Time.deltaTime;

        if (tempoRestante > 0)
        {
            AtualizarTexto();
        }
        else
        {
            jogoAtivo = false;
            SceneManager.LoadScene("NivelFalhado1");
        }
    }

    void AtualizarTexto()
    {
        textoTemporizador.text = "Tempo: " + Mathf.Ceil(tempoRestante).ToString() + "s";
    }

    public void PararTemporizador()
    {
        jogoAtivo = false;
    }
}