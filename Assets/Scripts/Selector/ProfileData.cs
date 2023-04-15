using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData
{
    private string username;
    private int levelUnlocked;
    private int highscore;
    //will need to add achivement here later

    public void CreateProfile(string name)
    {
        username = name;
        levelUnlocked = 1;
        highscore = 0;
        Debug.Log("Profile " + name + " created sucessfully");
    }

    public int GetHighscore()
    {
        return highscore;
    }

}
