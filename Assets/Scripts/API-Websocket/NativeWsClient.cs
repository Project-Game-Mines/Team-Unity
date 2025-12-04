using System.Collections;
using UnityEngine;
using NativeWebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class GameWebSocket : MonoBehaviour
{
    WebSocket ws;
    private int messageType;

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

        messageType = 0;
        yield return ws.SendText(msg.ToString());
        
    }

    public void GameStep(string matchId, int cell)
    {
        StartCoroutine(SendGameStep(matchId, cell));
    }

    private IEnumerator SendGameStep(string matchId, int cell)
    {
        JObject msg = new JObject
        {
            ["event"] = "GAME_STEP",
            ["data"] = new JObject
            {
                ["match_id"] = matchId,
                ["cell"] = cell
            }
        };

        yield return ws.SendText(msg.ToString());
        
    }

    public async void SendCashout(string matchId)
    {
        JObject msg = new JObject
        {
            ["event"] = "GAME_CASHOUT",
            ["data"] = new JObject
            {
                ["match_id"] = matchId
            }
        };

        await ws.SendText(msg.ToString());
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
                GameManager.match.active = true;
                Debug.Log(GameManager.match.matchId + "8888");
                break;
            case "STEP_RESULT":
                GameManager.matchStep = JsonUtility.FromJson<MatchStep>(message);
                Debug.Log(GameManager.match.active);
                Debug.Log(GameManager.matchStep.step);
                Debug.Log(GameManager.matchStep.isMine);
                break;
            
        }
    }
}