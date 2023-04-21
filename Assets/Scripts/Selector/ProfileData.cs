using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData
{
    private int userID;

    private string username;
    private int levelUnlocked;
    private int highscore;
    private ProfilesManager manager;
    //will need to add achivement here later

    public void CreateProfile(string name)
    {
        manager = GameObject.Find("ProfilesManager").GetComponent<ProfilesManager>();
        //if (PlayerPrefs.HasKey("totalProfiles"))
        //{
        //    userID = PlayerPrefs.GetInt("totalProfiles") + 1; //increment to new user id
        //    PlayerPrefs.SetInt("totalProfiles", userID); //set the total to the new user id
        //    Debug.Log("no more ini " + userID); 
        //}
        //else
        //{
        //    userID = 1; //set user id to 1
        //    PlayerPrefs.SetInt("totalProfiles", userID); //set initial total to 1
        //    Debug.Log("First time ini " + userID);
        //}

        userID = PlayerPrefs.GetInt("ID", 1);
        Debug.Log("Current id " + userID);

        username = name;
        levelUnlocked = 1;
        highscore = 0;

        PlayerPrefs.SetString("Username" + userID, username);
        PlayerPrefs.SetInt("levelUnlocked" + userID, levelUnlocked);
        PlayerPrefs.SetInt("Highscore" + userID, highscore);

        manager.UpdateManagerName(PlayerPrefs.GetString("Username" + userID));

        PlayerPrefs.SetInt("ID", userID + 1); //increment the id ready for next time we use it

        Debug.Log("Profile " + name + " created sucessfully");
        Debug.Log("Next id " + PlayerPrefs.GetInt("ID"));

    }

    public int GetHighscore()
    {
        return highscore;
    }
}
