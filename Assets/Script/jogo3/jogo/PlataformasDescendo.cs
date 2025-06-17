using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverPlataformas : MonoBehaviour
{
    public GameObject[] plataformas; // arraste aqui todas as plataformas (Platform, Platform (1), etc)
    public float velocidade = 1.0f;

    void Update()
    {
        foreach (GameObject plataforma in plataformas)
        {
            plataforma.transform.Translate(Vector2.down * velocidade * Time.deltaTime);
        }
    }
}
