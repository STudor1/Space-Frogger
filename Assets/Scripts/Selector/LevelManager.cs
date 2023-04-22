using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int selectedProfile;
    private int levelsUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        selectedProfile = int.Parse(ProfilesManager.selectedProfile);
        Debug.Log("Playing as " + PlayerPrefs.GetString("Username" + selectedProfile));
        Debug.Log("Levels unlocked " + PlayerPrefs.GetInt("levelUnlocked" + selectedProfile));
        levelsUnlocked = PlayerPrefs.GetInt("levelUnlocked" + selectedProfile);
        Debug.Log("Highscore " + PlayerPrefs.GetInt("Highscore" + selectedProfile));
    }

    
}
