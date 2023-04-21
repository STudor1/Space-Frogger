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
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject canvas;

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

        //we need to do this whenever a new profile has been added 

        //this creates a new button under our test profiles button
        Vector3 pos = new Vector3(960, 540, 0);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetPositionAndRotation(pos, rot);
        button.transform.SetParent(canvas.transform);//Setting button parent

    }
    public void UpdateManagerName(string username)
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
        Debug.Log("There are " + totalProfiles + " profiles " + username);

        //we need to do this whenever a new profile has been added 

        //this creates a new button under our test profiles button
        Vector3 pos = new Vector3(960, 540, 0);
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetPositionAndRotation(pos, rot);
        button.transform.SetParent(canvas.transform);//Setting button parent

    }
    //Use this to reset everything
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data has been reset");
    }
}
