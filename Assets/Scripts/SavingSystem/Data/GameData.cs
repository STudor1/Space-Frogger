using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int deathCount;
    public string username;

    public GameData(string user)
    {
        this.deathCount = 0;
        username = user;
    }
}
