// Script APIManager.cs (anexado a um GameObject)
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class APIManager : MonoBehaviour 
{
    [SerializeField] private PlayerDisplay playerDisplay;
    public  Player playerAPI; 

    public void StartFetchingPlayer() 
    {
        StartCoroutine(FetchPlayer()); 
    }

    private IEnumerator FetchPlayer() 
    {
        
        UnityWebRequest request = UnityWebRequest.Get("https://mines-back.onrender.com/users/find/{users_id}?user_id=692f1d6cedc0062c96dd0dc5");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) 
        {
            string json = request.downloadHandler.text;
            
            playerAPI = JsonUtility.FromJson<Player>(json); 
            if (playerDisplay != null)
            {
                playerDisplay.UpdatePlayerDisplay();
            }
        }
        else
        {
            Debug.LogError("Erro na requisição: " + request.error);
        }
        
    }

    public void UpdatePlayerBalance()
    {
        StartCoroutine(FetchBalance());
    }

    private IEnumerator FetchBalance()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://mines-back.onrender.com/wallet/balance?user_id=692f1d6cedc0062c96dd0dc5");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) 
        {
            string json = request.downloadHandler.text;
            
            playerAPI = JsonUtility.FromJson<Player>(json); 
            if (playerDisplay != null)
            {
                playerDisplay.UpdateBalance();
            }
        }
        else
        {
            Debug.LogError("Erro na requisição: " + request.error);
        }
    }
    
}
