// Script APIManager.cs (anexado a um GameObject)
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class APIManager : MonoBehaviour
{
    [SerializeField] private PlayerDisplay playerDisplay;
    



    //-----------------------------Player Fetch--------------------------------//
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

            GameManager.player = JsonUtility.FromJson<Player>(json);
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
    //----------------Balance Update----------------//
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

            GameManager.player = JsonUtility.FromJson<Player>(json);
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

    //---------------------Start Game----------------------------//

    [System.Serializable]
    public class GameStartData
    {
        public string user_id;
        public float bet_amount;
        public int total_mines;
    }

    public void RequestStartGame(string userId, float betAmount, int totalMines)
    {
        // Certifique-se de que a corrotina seja iniciada
        StartCoroutine(StartGame(userId, betAmount, totalMines));
        
    }

    private IEnumerator StartGame(string userId, float betAmount, int totalMines)
    {
        string url = "https://mines-back.onrender.com/game/start";

        // Crie o objeto de dados a ser enviado
        GameStartData dataToSend = new GameStartData
        {
            user_id = userId,
            bet_amount = betAmount,
            total_mines = totalMines
        };

        // Converta o objeto C# para uma string JSON
        string jsonPayload = JsonUtility.ToJson(dataToSend);

        // Converte a string JSON em bytes
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        // 2. Cria o UnityWebRequest usando o método POST
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // 3. Define o corpo da requisição (payload)
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // 4. Define o DownloadHandler (para receber a resposta da API)
        request.downloadHandler = new DownloadHandlerBuffer();

        // 5. Define o Content-Type para informar à API que estamos enviando JSON
        request.SetRequestHeader("Content-Type", "application/json");
        

        // Envia a requisição
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Jogo iniciado com sucesso! Resposta: " + request.downloadHandler.text);
            UpdatePlayerBalance();
            //-------Generate Match ID-------//
            string json = request.downloadHandler.text;
            GameManager.match = JsonUtility.FromJson<Match>(json);
            Debug.Log(GameManager.match.matchId);
        }

        // Exemplo de como desserializar a resposta da API (se necessário)
        // GameStartResponse responseData = JsonUtility.FromJson<GameStartResponse>(request.downloadHandler.text);
        // Debug.Log("ID do Jogo (Game ID): " + responseData.game_id);
    
        else
        {
            Debug.LogError("Erro ao iniciar o jogo: " + request.error);
            Debug.LogError("Resposta da API (se houver): " + request.downloadHandler.text);
        }
    }



}
