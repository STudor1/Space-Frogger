using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement Service",
    menuName = "Achievements/Achievement Service", order = 1)]
/// <summary>
/// This is the observer, and frogger is the subject
/// </summary>
public class AchievementService : MonoBehaviour
{
    [SerializeField] private Achievement[] achievements; //list of our achievements
    private Achievement DIE_10_TIMES;

    //Set all the achievements to their names;
    private void Awake()
    {
        DIE_10_TIMES = achievements[0];
    }

    //subscribe to event
    private void OnEnable()
    {
        Frogger.OnDeath += OnDeath;
    }

    //unsubscribe from event
    private void OnDisable()
    {
        Frogger.OnDeath -= OnDeath;
    }

    private void OnDeath(int deathCount)
    {
        if (deathCount == 10) UnlockAchievement(DIE_10_TIMES);
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
