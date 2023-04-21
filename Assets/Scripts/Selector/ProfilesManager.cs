using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will be used to keep a record of all profiles and be used to display the profiles in the level selector page
/// </summary>
public class ProfilesManager : MonoBehaviour
{
    private ProfileData[] profiles;
    private int totalProfiles;

    public void UpdateManager()
    {
        if (PlayerPrefs.GetInt("ID") == 0)
        {
            totalProfiles = PlayerPrefs.GetInt("ID");
        }
        else
        {
            totalProfiles = PlayerPrefs.GetInt("ID") - 1;
        }
        
        //profiles = GetComponents<ProfileData>();
        Debug.Log("There are " + totalProfiles + " profiles");
    }

    //Use this to reset everything
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data has been reset");
    }
}
