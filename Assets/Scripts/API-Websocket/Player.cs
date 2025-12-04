using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Player
{
    public string id;
    public string name;
    public string created_at;
    public float balance;
}

public class Match
{
    public string matchId;
    public bool active;
}

public class MatchStep
{
    public int step = 0;
    public bool isMine;
}

public class baseMessage
{
    public string @event;
}





