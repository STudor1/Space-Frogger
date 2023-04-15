using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateProfile : MonoBehaviour
{
    [SerializeField] private GameObject usernameField;
    [SerializeField] private ProfilesManager manager;
    private ProfileData newProfile;

    public void AddProfile()
    {
        string text = usernameField.GetComponent<TMP_InputField>().text;

        if (text != "")
        {
            newProfile = new ProfileData(); //create a new profile
            newProfile.CreateProfile(text); //add the data
        }
        else
        {
            Debug.Log("Enter a valid name");
        }
    }

    public void UpdateManager()
    {
        manager.UpdateManager();
    }

}
