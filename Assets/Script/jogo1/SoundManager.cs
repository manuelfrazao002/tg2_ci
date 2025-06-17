using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    public string[] destroyOnScenes;
    private static bool isPlaying = false;

    void Awake()
    {
        if (!isPlaying)
        {
            DontDestroyOnLoad(gameObject);
            isPlaying = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in destroyOnScenes)
        {
            if (scene.name == sceneName)
            {
                isPlaying = false;
                SceneManager.sceneLoaded -= OnSceneLoaded;
                Destroy(gameObject);
                break;
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
