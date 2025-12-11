using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
//classe para guardar as informaçoes do jogador
public class Player
{
    public string id;
    public string name;
    public string created_at;
    public float balance;
}
//classe para armazenar o id e estado da match
public class Match
{
    
    public string matchId;
    public bool active;
    
}
//classe para verificar cada etapa da match
public class MatchStep
{
    public int step = 0;
    public bool isMine;
    public float prize;
    
}
//classe criada para verificar ao final da partida as posiçoes das minas para mostrar os icones no fim do jogo
public class MinesPosition
{
    public List <int> mines_positions;
}
//classe para tratar a ws.OnMessage
public class baseMessage
{
    public string @event;
}





