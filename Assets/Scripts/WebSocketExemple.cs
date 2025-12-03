// using System;
// using System.Text;
// using System.Threading.Tasks;
// using UnityEngine;
// using NativeWebSocket;
//
// public class WebSocketExemple : MonoBehaviour
// {
//     private WebSocket websocket;
//
//     async void Start()
//     {
//         // Substitua pela URL do seu servidor WebSocket
//         websocket = new WebSocket("wss://echo.websocket.org");
//
//         // Evento chamado quando a conexão é aberta
//         websocket.OnOpen += () =>
//         {
//             Debug.Log("Conexão WebSocket aberta!");
//         };
//
//         // Evento chamado quando uma mensagem é recebida
//         websocket.OnMessage += (bytes) =>
//         {
//             string message = Encoding.UTF8.GetString(bytes);
//             Debug.Log("Mensagem recebida: " + message);
//         };
//
//         // Evento chamado quando a conexão é fechada
//         websocket.OnClose += (e) =>
//         {
//             Debug.Log("Conexão fechada!");
//         };
//
//         // Evento chamado em caso de erro
//         websocket.OnError += (errMsg) =>
//         {
//             Debug.LogError("Erro WebSocket: " + errMsg);
//         };
//
//         // Conecta ao servidor
//         await websocket.Connect();
//     }
//
//     void Update()
//     {
// #if !UNITY_WEBGL || UNITY_EDITOR
//         // Necessário para processar mensagens fora do WebGL
//         websocket.DispatchMessageQueue();
// #endif
//
//         // Exemplo: enviar mensagem ao pressionar espaço
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             SendMessageToServer("Olá servidor!");
//         }
//     }
//
//     public async void SendMessageToServer(string message)
//     {
//         if (websocket.State == WebSocketState.Open)
//         {
//             await websocket.SendText(message);
//             Debug.Log("Mensagem enviada: " + message);
//         }
//         else
//         {
//             Debug.LogWarning("Não é possível enviar, conexão não está aberta.");
//         }
//     }
//
//     private async void OnApplicationQuit()
//     {
//         if (websocket != null)
//         {
//             await websocket.Close();
//         }
//     }
// }
