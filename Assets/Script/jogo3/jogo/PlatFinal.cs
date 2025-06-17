using UnityEngine;
using UnityEngine.SceneManagement;

public class PlataformaFinal : MonoBehaviour
{
    public string cenaFinal = "Nivel3-6";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playerr")) // certifique-se que "Bola" tem a tag "Player"
        {
            SceneManager.LoadScene(cenaFinal);
        }
    }
}
