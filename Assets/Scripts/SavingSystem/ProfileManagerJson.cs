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
public class ProfileManagerJson : MonoBehaviour
{
    private ProfileData[] profiles;
    private int totalProfiles;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject canvas;
    private int currentY;
    public static string selectedProfile;
    [SerializeField] private SaveData data;
    private int nextID;

    public void Start()
    {
        //if (PlayerPrefs.GetInt("ProfileNo") == 0)
        //{
        //    totalProfiles = PlayerPrefs.GetInt("ProfileNo");
        //}
        //else
        //{
        //    totalProfiles = PlayerPrefs.GetInt("ProfileNo");
        //}

        nextID = PlayerPrefs.GetInt("ProfileNo");
        Debug.Log("There are " + nextID + " json profiles");

        if (nextID == 0)
        {
            currentY = 540 + 120;
            Debug.Log(currentY);
        }
        else
        {
            for (int i = 1; i < nextID; i++)
            {
                UserProfile user = data.LoadProfile(i);

                //i--;
                Vector3 pos = new Vector3(960, 540 + (-120 * (i-1)), 0);
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.transform.SetPositionAndRotation(pos, rot);
                button.transform.SetParent(canvas.transform);//Setting button parent
                button.GetComponentInChildren<TMP_Text>().text = user.username;
                button.name = (i).ToString();
                button.GetComponent<Button>().onClick.AddListener(SelectProfile);
                currentY = 540 + (-120 * (i-1));

            }
        }
    }

    public void UpdateManagerName(string username, int id)
    {
        //if (PlayerPrefs.GetInt("ID") == 0)
        //{
        totalProfiles = PlayerPrefs.GetInt("ProfileNo") - 1;
        //}
        //else
        //{
        //    totalProfiles = PlayerPrefs.GetInt("ID") - 1;
        //}

        //profiles = GetComponents<ProfileData>();
        Debug.Log("There are " + totalProfiles + " json profiles " + username);
        Debug.Log(currentY);
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
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("ProfileNo");
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
