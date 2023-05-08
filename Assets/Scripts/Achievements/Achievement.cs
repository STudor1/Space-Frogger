using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", 
    menuName = "Achievements/Achievement Data", order = 1)]

[System.Serializable]
public class Achievement : ScriptableObject
{
    [SerializeField] public string achID;
    [SerializeField] public string achTitle;
    [SerializeField] public string achDescription;

    private bool unlocked = false;

    public bool Unlocked => unlocked;

    public void Unlock()
    {
        Debug.Log("Unlocked " + achTitle + "\n" + achDescription);
        unlocked = true;
    }
}
