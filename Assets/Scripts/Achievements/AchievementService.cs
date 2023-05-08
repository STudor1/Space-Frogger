using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Achievement Service",
//    menuName = "Achievements/Achievement Service", order = 1)]
/// <summary>
/// This is the observer, and frogger is the subject
/// </summary>
public class AchievementService : MonoBehaviour
{
    [SerializeField] private Achievement[] achievements; //list of our achievements
    private Achievement DIE_10_TIMES;
    private Achievement GOTTA_GO_FAST;
    private Achievement DEAD_FROGS_TELL_NO_TALES;
    private int selectedProfile;
    private SaveData data;
    UserProfile currentUser;

    //Set all the achievements to their names;
    private void Awake()
    {
        DIE_10_TIMES = achievements[0];
        GOTTA_GO_FAST = achievements[1];
        DEAD_FROGS_TELL_NO_TALES = achievements[2];

        
    }

    //subscribe to event
    private void OnEnable()
    {
        GameManager.OnDeath += OnDeath;
        GameManager.OnHomeEnter += OnHomeEnter;
        GameManager.OnLevelFinish += OnLevelFinish;
    }

    //unsubscribe from event
    private void OnDisable()
    {
        GameManager.OnDeath -= OnDeath;
        GameManager.OnHomeEnter -= OnHomeEnter;
        GameManager.OnLevelFinish -= OnLevelFinish;
    }

    private void OnDeath(int deathCount)
    {
        if (deathCount == 10) UnlockAchievement(DIE_10_TIMES);
    }

    private void OnHomeEnter(int timeLeft)
    {
        if (timeLeft > 19) UnlockAchievement(GOTTA_GO_FAST);
    }

    private void OnLevelFinish(int livesLeft)
    {
        if (livesLeft == 3) UnlockAchievement(DEAD_FROGS_TELL_NO_TALES);
    }

    private void UnlockAchievement(Achievement achievement)
    {
        selectedProfile = int.Parse(ProfileManagerJson.selectedProfile);
        data = FindObjectOfType<SaveData>();
        currentUser = data.LoadProfile(selectedProfile);
        Debug.Log("Saving ach for " + currentUser.username);

        bool unlocked = CheckIfUnlocked(achievement);

        if (!unlocked)
        {
            //unlock ach
            foreach (Achievement ach in currentUser.achievements)
            {
                if (ach.achTitle == achievement.achTitle)
                {
                    ach.Unlock();
                    data.SaveTheData(currentUser, currentUser.id);
                }
            }
        }
        return;
    }

    private bool CheckIfUnlocked(Achievement achievement)
    {
        return achievement.Unlocked;
    }
}
