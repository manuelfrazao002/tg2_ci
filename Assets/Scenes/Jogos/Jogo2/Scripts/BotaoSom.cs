using UnityEngine;

public class BotaoSom : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somBotao;

    public void TocarSom()
    {
        if (audioSource != null && somBotao != null)
        {
            audioSource.PlayOneShot(somBotao);
        }
    }
}

