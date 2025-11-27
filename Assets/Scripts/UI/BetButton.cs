using UnityEngine;

public class BetButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void StartGame()
    {
        if (!gameManager.active)
        {
            gameManager.StartGame();
        }
        
    }
    
    

    
}
