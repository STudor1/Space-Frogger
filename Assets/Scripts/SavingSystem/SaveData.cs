using System.Collections.Generic;
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

                string json = JsonUtility.ToJson(userProfile, true);

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(json);
                    }
                }

                

                profileNo++;
                PlayerPrefs.SetInt("ProfileNo", profileNo);
                manager.UpdateManagerName(userProfile.username, userProfile.id);

            }
            else
            {
                string error = "Max profiles reached";
                usernameField.GetComponent<TMP_InputField>().text = error;
            }

        }
        else
        {
            string valid = "Enter a valid name";
            usernameField.GetComponent<TMP_InputField>().text = valid;
        }
    }

    public UserProfile LoadProfile(int id)
    {
        string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ProfileData" + id.ToString() + ".json";
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();

            UserProfile user = JsonUtility.FromJson<UserProfile>(json);
            Debug.Log("Loading " + user.username);
            Debug.Log("Loading " + user.highscore);
            foreach (Achievement ach in user.achievements)
            {
                if (ach.Unlocked == false)
                {
                    Debug.Log("Loading: Achievement " + ach.achTitle + " is not unlocked");
                }
                else
                {
                    Debug.Log("Loading: Achievement " + ach.achTitle + " is unlocked");
                }
            }
            return user;
        }

        
    }

    public void SaveTheData(UserProfile profile, int id)
    {
        string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "ProfileData" + id.ToString() + ".json";

        string json = JsonUtility.ToJson(profile, true);
        Debug.Log("Then here ");
        foreach (Achievement ach in profile.achievements)
        {
            Debug.Log("Saving: Achievement " + ach.achTitle + " is unlocked " + ach.Unlocked);
        }

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
    [SerializeField] public List<Achievement> achievements;

    //what i can try for this is create a dic with the ach id and unlocked or not and pass it as keys
    // see this https://www.youtube.com/watch?v=aUi9aijvpgs&t=921s
    public UserProfile(int id, string username)
    {
        this.id = id;
        this.username = username;
        deathCount = 0;
        levelUnlocked = 1;
        highscore = 0;
        achievements = new List<Achievement>(Resources.LoadAll<Achievement>("Achievements"));
    }
}
