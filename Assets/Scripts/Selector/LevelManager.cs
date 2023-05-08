using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text achText;
    private int selectedProfile;
    private int levelsUnlocked;
    private SaveData data;
    private string achString;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<SaveData>();
        selectedProfile = int.Parse(ProfileManagerJson.selectedProfile);
        Debug.Log(selectedProfile);

        UserProfile currentUser = data.LoadProfile(selectedProfile);

        levelsUnlocked = currentUser.levelUnlocked;
        //Debug.Log("Playing as " + currentUser.username);
        //Debug.Log("Highscore " + currentUser.highscore);


        foreach (Achievement ach in currentUser.achievements)
        {
            string achUnlocked = PlayerPrefs.GetString("AchUnlocked" + currentUser.id + ach.achID);
            //Debug.Log("hehe" + achUnlocked + "mate");
            string achToAdd = "-Achievement " + ach.achTitle + " is not unlocked" + "\n" + ach.achDescription;

            if (achUnlocked != "")
            {
                //Debug.Log("here?");
                achString = achString + "\n" + achUnlocked;
            }
            else if (achUnlocked == achToAdd)
            {

            }
            else
            {
                if (ach.Unlocked == false)
                {
                    if (achString == null)
                    {
                        achString = achToAdd;

                    }
                    else
                    {
                        achString = achString + "\n" + achToAdd;
                    }
                }
            }

            
            //else
            //{
            //    achString = achString + "\n" + "-Achievement " + ach.achTitle + " is unlocked" + "\n" + ach.achDescription;
            //}
        }

        achText.SetText(achString);
    }


}
