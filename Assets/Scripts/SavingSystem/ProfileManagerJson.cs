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
    [SerializeField] private SaveData data;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject canvas;
    public static string selectedProfile;
    private int totalProfiles;
    private int currentY;
    private int nextID;

    //This generates all the buttons at the start of scene
    public void Start()
    {
        nextID = PlayerPrefs.GetInt("ProfileNo");

        if (nextID == 0)
        {
            currentY = 660 + 120;
        }
        else
        {
            for (int i = 1; i < nextID; i++)
            {
                UserProfile user = data.LoadProfile(i);

                string profileText = user.username + "   Highscore: " + user.highscore + "   Lvl: " + user.levelUnlocked; 

                Vector3 pos = new Vector3(960, 660 + (-120 * (i-1)), 0);
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject button = (GameObject)Instantiate(buttonPrefab);
                button.transform.SetPositionAndRotation(pos, rot);
                button.transform.SetParent(canvas.transform);//Setting button parent
                button.GetComponentInChildren<TMP_Text>().text = profileText;
                button.name = (i).ToString();
                button.GetComponent<Button>().onClick.AddListener(SelectProfile);
                currentY = 660 + (-120 * (i-1));

            }
        }
    }

    //This is called when a new button should be added, after the profile has been created
    public void UpdateManagerName(string username, int id)
    {
        totalProfiles = PlayerPrefs.GetInt("ProfileNo") - 1;
        
        UserProfile user = data.LoadProfile(id);
        string profileText = user.username + "   Highscore: " + user.highscore + "   Lvl: " + user.levelUnlocked;

        if (totalProfiles < 4)
        {
            //this creates a new button under our test profiles button
            Vector3 pos = new Vector3(960, currentY - 120, 0); //this is 0 for some reason next at y + 120
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.name = id.ToString();
            button.transform.SetPositionAndRotation(pos, rot);
            button.transform.SetParent(canvas.transform);//Setting button parent
            button.GetComponentInChildren<TMP_Text>().text = profileText;
            button.GetComponent<Button>().onClick.AddListener(SelectProfile);
            currentY = currentY - 120;
        }
    }

    //Use this to reset profiles
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("ProfileNo");
        Debug.Log("Data has been reset");
    }

    //This selects the id of the profile we are going to be playing as
    public void SelectProfile()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        string objectName = selectedObject.name;
        selectedProfile = objectName;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
