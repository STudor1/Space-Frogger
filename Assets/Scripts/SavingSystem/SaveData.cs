using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class SaveData : MonoBehaviour
{
    [SerializeField] private GameObject usernameField;

    private UserProfile userProfile;
    //private string path = "";
    private string persistentPath = "";
    private int profileNo;
    private ProfileManagerJson manager;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void SetPaths()
    {
        //path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        profileNo = PlayerPrefs.GetInt("ProfileNo");
        if (profileNo == 0)
        {
            profileNo++;
            PlayerPrefs.SetInt("ProfileNo", profileNo);
        }
        Debug.Log(profileNo);
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ProfileData" + profileNo.ToString() + ".json";
    }

    public void SaveProfile()
    {
        manager = GameObject.Find("ProfilesManager").GetComponent<ProfileManagerJson>();
        string text = usernameField.GetComponent<TMP_InputField>().text;

        if (text != "")
        {
            SetPaths();
            string savePath = persistentPath;
            profileNo = PlayerPrefs.GetInt("ProfileNo");

            if (profileNo < 4)
            {
                userProfile = new UserProfile(profileNo, text); //create a new profile
                Debug.Log("Saving Data at " + savePath);

                string json = JsonUtility.ToJson(userProfile);
                Debug.Log(json);

                using (StreamWriter writer = new StreamWriter(savePath))
                {
                    writer.Write(json);
                }

                profileNo++;
                PlayerPrefs.SetInt("ProfileNo", profileNo);
                manager.UpdateManagerName(userProfile.username, userProfile.id);

            }
            else
            {
                Debug.Log("Delete a profile to add a new one");
            }
            
        }
        else
        {
            Debug.Log("Enter a valid name");
        }
    }

    public UserProfile LoadProfile(int id)
    {
        string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ProfileData" + id.ToString() + ".json";
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();

            UserProfile user = JsonUtility.FromJson<UserProfile>(json);
            return user;
        }

        
    }

    public void SaveTheData(UserProfile profile, int id)
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ProfileData" + id.ToString() + ".json";

        string json = JsonUtility.ToJson(profile);
        Debug.Log(json);

        using (StreamWriter writer = new StreamWriter(savePath))
        {
            writer.Write(json);
        }

    }
}

[System.Serializable]
public class UserProfile
{
    public int id;
    public string username;
    public int deathCount;
    public int levelUnlocked;
    public int highscore;
    public Achievement[] achievements;

    public UserProfile(int id, string username)
    {
        this.id = id;
        this.username = username;
        deathCount = 0;
        levelUnlocked = 1;
        highscore = 0;
        achievements = Resources.LoadAll<Achievement>("Achievements");
        if (achievements == null)
        {
            Debug.Log("Sad");
        }
    }
}
