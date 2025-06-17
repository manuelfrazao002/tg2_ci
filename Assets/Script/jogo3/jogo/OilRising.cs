using UnityEngine;
using UnityEngine.SceneManagement;

public class OleoPerigoso : MonoBehaviour
{
    public string cenaDerrota = "CenaDerrota"; // Nome da cena de reinício ou derrota

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playerr")) // Certifique-se de que a Bola tem a tag "Player"
        {
            SceneManager.LoadScene(cenaDerrota);
        }
    }
}
