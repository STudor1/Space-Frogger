using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will be used to keep a record of all profiles and be used to display the profiles in the level selector page
/// </summary>
public class ProfilesManager : MonoBehaviour
{
    private ProfileData[] profiles;

    public void UpdateManager()
    {
        //profiles = GetComponents<ProfileData>();
        Debug.Log("There are " + /*profiles.Length +*/ " profiles");
    }
}
