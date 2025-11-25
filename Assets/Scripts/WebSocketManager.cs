using System;
using UnityEngine;
using NativeWebSocket;

public class WebSocketManager : MonoBehaviour
{
    WebSocket websocket;

    async void Start()
    {
        // Initialize WebSocket with the server URL
        websocket = new WebSocket("ws://localhost:8080");
        // ws://localhost:8080

        // Event handlers for connection, errors, and messages
        websocket.OnOpen += () => Debug.Log("Connection opened!");
        websocket.OnError += (e) => Debug.Log($"Error: {e}");
        websocket.OnClose += (e) => Debug.Log("Connection closed!");
        websocket.OnMessage += (bytes) =>
        {
            Debug.Log("Message received!");
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log($"Message: {message}");
        };

        // Connect to the WebSocket server
        await websocket.Connect();

        // Send periodic messages
        InvokeRepeating("SendMessage", 0.0f, 0.3f);
    }

    void Update()
    {
        // Dispatch WebSocket messages (required for WebGL)
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void SendMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("Hello from Unity!");
        }
    }

    async void OnApplicationQuit()
    {
        // Close the WebSocket connection on application exit
        await websocket.Close();
    }
}
