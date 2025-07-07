using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneButtonHandler_Game4 : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLastLevel()
    {
        if (!string.IsNullOrEmpty(NivelManager.ultimoNivel))
        {
            SceneManager.LoadScene(NivelManager.ultimoNivel);
        }
        else
        {
            Debug.LogWarning("Nenhum Nivel anterior registado.");
        }
    }
}
