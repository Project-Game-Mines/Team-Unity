using System;
using System.Collections;
using UnityEngine;
using NativeWebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class GameWebSocket : MonoBehaviour
{
    WebSocket ws;
    [SerializeField] private GameManager gameManager;
    public string latestWsResponse = null;
    public bool responseReceived = false;

    async void Start()
    {
        ws = new WebSocket("wss://mines-back.onrender.com/ws/692f1d6cedc0062c96dd0dc5");

        ws.OnOpen += () =>
        {
            Debug.Log("Conectado ao servidor WebSocket!");
        };

        ws.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Mensagem recebida: " + message);
            OnWSMessage(message);
        };

        ws.OnError += (e) =>
        {
            Debug.LogError("Erro no WebSocket: " + e);
        };

        ws.OnClose += (e) =>
        {
            Debug.Log("WebSocket fechado: " + e);
        };

        await ws.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        ws?.DispatchMessageQueue();
        #endif
    }

    public void StartGame(string userId, int betAmount, int totalMines)
    {
        StartCoroutine(SendGameStart(userId, betAmount,totalMines));
    }
    private IEnumerator SendGameStart(string userId, int betAmount, int totalMines)
    {
        JObject msg = new JObject
        {
            ["event"] = "GAME_START",
            ["data"] = new JObject
            {
                ["user_id"] = userId,
                ["bet_amount"] = betAmount,
                ["total_mines"] = totalMines
            }
        };
        
        yield return ws.SendText(msg.ToString());
        
    }

    public void GameStep(string matchId, int cell, Action<string> callback)
    {
        StartCoroutine(SendGameStep(matchId, cell,  callback));
    }

    private IEnumerator SendGameStep(string matchId, int cell, Action<string> callback)
    {
        gameManager.mineButtonActive = false;
        JObject msg = new JObject
        {
            ["event"] = "GAME_STEP",
            ["data"] = new JObject
            {
                ["match_id"] = matchId,
                ["cell"] = cell
            }
        };

        // Reseta o estado da resposta antes de enviar
        latestWsResponse = null;
        responseReceived = false;

        // A. Envia a mensagem
        yield return ws.SendText(msg.ToString());

        // B. Lógica de espera: Aguarda a resposta do servidor
        float timeout = 5f; // Tempo limite para a resposta
        float startTime = Time.time;
    
        // Espera até que a resposta chegue OU o tempo limite seja atingido.
        while (!responseReceived && Time.time < startTime + timeout)
        {
            // Seu listener de WebSocket (OnMessageReceived) deve setar
            // 'responseReceived = true' e 'latestWsResponse = RESPOSTA_JSON'
            // quando receber o evento GAME_STEP ou GAME_LOSE para este passo.
            yield return null; 
        }

        // C. Chama o callback com a resposta recebida
        if (responseReceived && latestWsResponse != null)
        {
            callback?.Invoke(latestWsResponse);
        }
        else
        {
            // Trate o erro de timeout
            Debug.LogError("Tempo limite de resposta do servidor GameStep excedido.");
            // Opcional: Chama o callback com um erro se for necessário
        }
    }

    public void ChashOut(string matchId)
    {
        StartCoroutine(SendCashout(matchId));
    }
    private IEnumerator  SendCashout(string matchId)
    {
        JObject msg = new JObject
        {
            ["event"] = "GAME_CASHOUT",
            ["data"] = new JObject
            {
                ["match_id"] = matchId
            }
        };

        yield return ws.SendText(msg.ToString());
    }

    async void OnDestroy()
    {
        if (ws != null && ws.State == WebSocketState.Open)
        {
            await ws.Close();
        }
    }
    
    
    private void OnWSMessage(string message) 
    {
        baseMessage msg = JsonUtility.FromJson<baseMessage>(message);
        switch (msg.@event)
        {
            case "GAME_STARTED":
                GameManager.match = JsonUtility.FromJson<Match>(message);
                gameManager.UpdateBalance();
                GameManager.match.active = true;
                Debug.Log(GameManager.match.matchId + "8888");
                break;
            
            case "STEP_RESULT":
                latestWsResponse = message;
                responseReceived = true;
                GameManager.matchStep = JsonUtility.FromJson<MatchStep>(message);
                Debug.Log(GameManager.matchStep.prize);
                break;
            
            case "GAME_LOSE":
                GameManager.minesPositions = JsonUtility.FromJson<MinesPosition>(message);
                Debug.Log(GameManager.minesPositions);
                gameManager.GameOver();
                latestWsResponse = message;
                responseReceived = true;
                break;
            
            case "GAME_CASHOUT":
                GameManager.minesPositions = JsonUtility.FromJson<MinesPosition>(message);
                gameManager.GameOver();
                gameManager.UpdateBalance();
                break;
            
            case "GAME_WIN":
                GameManager.minesPositions = JsonUtility.FromJson<MinesPosition>(message);
                GameManager.match = JsonUtility.FromJson<Match>(message);
                
                gameManager.GameOver();
                gameManager.UpdateBalance();
                
                break;

            default:
                Debug.Log($"Evento WS não tratado:");
                break;
        }
    }

    
}
