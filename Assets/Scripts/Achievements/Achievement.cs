using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", 
    menuName = "Achievements/Achievement Data", order = 1)]

public class Achievement : ScriptableObject
{
    [SerializeField] private string achID;
    [SerializeField] private string achTitle;
    [SerializeField] private string achDescription;

    private bool unlocked = false;

    public bool Unlocked => unlocked;

    public void Unlock()
    {
        Debug.Log("Unlocked " + achTitle + "\n" + achDescription);
        unlocked = true;
    }

}
