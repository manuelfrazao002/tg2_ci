using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public bool isHealthy; // Define se essa comida � saud�vel ou n�o

    private FoodGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<FoodGameManager>();
    }

    public void OnHealthyButton()
    {
        gameManager.Answer(true, isHealthy);
    }

    public void OnUnhealthyButton()
    {
        gameManager.Answer(false, isHealthy);
    }
}
