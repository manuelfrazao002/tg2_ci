using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] levelButtons; // Atribua Level1Completed, Level2Completed, etc.

    void Start()
    {
        RefreshLevelStatus();
    }

    void Update()
    {
        // Atalho de teclado para limpar dados (opcional, só para testes)
        if (Input.GetKeyDown(KeyCode.R)) // R = Reset
        {
            ClearAllPlayerData();
        }
    }

    public void ClearAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        RefreshLevelStatus(); // Atualiza os checkmarks

        Debug.Log("Dados resetados!");
    }

    public void RefreshLevelStatus()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string levelKey = "Level" + (i + 1) + "Completed";
            Transform check = levelButtons[i].transform.Find("CheckMark");

            if (check != null)
            {
                // Só ativa o checkmark se o nível foi completado com todas as respostas corretas
                check.gameObject.SetActive(PlayerPrefs.GetInt(levelKey, 0) == 1);
            }
        }
    }
}