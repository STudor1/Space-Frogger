using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement Service",
    menuName = "Achievements/Achievement Service", order = 1)]
/// <summary>
/// This is the observer, and frogger is the subject
/// </summary>
public class AchievementService : ScriptableObject
{
    [SerializeField] private Achievement[] achievements; //list of our achievements



    private void OnEnable()
    {
        //Frogger.ach += UnlockAchievement;
    }

    private void OnDisable()
    {
        
    }

    private void UnlockAchievement(Achievement achievement)
    {
        bool unlocked = CheckIfUnlocked(achievement);

        if (!unlocked)
        {
            //unlock ach
            achievement.Unlock();
        }return;
    }

    private bool CheckIfUnlocked(Achievement achievement)
    {
        
        //do check here
        return achievement.Unlocked;
    }
}
