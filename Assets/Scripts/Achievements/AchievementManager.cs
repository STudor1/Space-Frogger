using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementManager : MonoBehaviour
{
    public List<Ach> achievements;

    public int deathCount;
    public float floating_point;


    public void InitializeAchievements()
    {
        if (achievements != null)
        {
            return;
        }

        achievements = new List<Ach>();
        achievements.Add(new Ach("Die twice", "Die 2 times.", (object o) => deathCount >= 2));
    }

    public void CheckAchCompletion()
    {
        if (achievements == null)
        {
            return;
        }

        foreach (var ach in achievements)
        {
            ach.UpdateCompletion();
        }
    }
}

public class Ach
{
    public string title;
    public string description;
    public Predicate<object> requirement;

    public bool isUnlocked;

    public Ach(string title, string description, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description;
        this.requirement = requirement;
    }

    public void UpdateCompletion()
    {
        if (isUnlocked)
        {
            return;
        }

        if (RequirementsMet())
        {
            Debug.Log($"{title}: {description}");
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }

}
