using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MudarCena(string NomeCena)
    {
        SceneManager.LoadScene(NomeCena);
    }

}
