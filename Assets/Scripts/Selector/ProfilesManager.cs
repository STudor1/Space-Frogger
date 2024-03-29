using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This will be used to keep a record of all profiles and be used to display the profiles in the level selector page
/// </summary>
public class ProfilesManager : MonoBehaviour
{
    private ProfileData[] profiles;
    private int totalProfiles;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject canvas;
    private int currentY;
    public static string selectedProfile;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ID") == 0)
        {
            totalProfiles = PlayerPrefs.GetInt("ID");
        }
        else
        {
            totalProfiles = PlayerPrefs.GetInt("ID") - 1;
        }
        //}
        //else
        //{
        //    totalProfiles = PlayerPrefs.GetInt("ID") - 1;
        //}

        //profiles = GetComponents<ProfileData>();

        if (totalProfiles != 0)
        {
            for (int i = 0; i < totalProfiles; i++)
            {
                Vector3 pos = new Vector3(960, 540 + (-120 * i), 0);
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.transform.SetPositionAndRotation(pos, rot);
                button.transform.SetParent(canvas.transform);//Setting button parent
                button.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetString("Username" + (i + 1));
                button.name = (i+1).ToString();
                button.GetComponent<Button>().onClick.AddListener(SelectProfile);
                currentY = 540 + (-120 * i);
                
            }
        }
        else
        {
            currentY = 540 + 120;
        }
    }


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
    public void UpdateManagerName(string username, int id)
    {
        //if (PlayerPrefs.GetInt("ID") == 0)
        //{
        totalProfiles = PlayerPrefs.GetInt("ID");
        //}
        //else
        //{
        //    totalProfiles = PlayerPrefs.GetInt("ID") - 1;
        //}

        //profiles = GetComponents<ProfileData>();
        Debug.Log("There are " + totalProfiles + " profiles " + username);

        //we need to do this whenever a new profile has been added 

        if (totalProfiles < 4)
        {
            //this creates a new button under our test profiles button
            Vector3 pos = new Vector3(960, currentY - 120, 0); //this is 0 for some reason next at y + 120
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.name = id.ToString();
            button.transform.SetPositionAndRotation(pos, rot);
            button.transform.SetParent(canvas.transform);//Setting button parent
            button.GetComponentInChildren<TMP_Text>().text = username;
            button.GetComponent<Button>().onClick.AddListener(SelectProfile);
            currentY = currentY - 120;
        }
        else
        {
            Debug.Log("Delete a profile to add another one");
        }
       

    }
    //Use this to reset everything
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data has been reset");
    }

    public void SelectProfile()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        string objectName = selectedObject.name;
        Debug.Log("Selected object name: " + objectName);
        selectedProfile = objectName;
        //Debug.Log(this.GetComponentInChildren<TMP_Text>().text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
