using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Frogger frogger;
    [SerializeField] private GameObject gameEnv;
    [SerializeField] private GameObject[] popUps;
    private int popUpIndex;
    private float waitTime = 2f;
    private float waitTime2 = 5f;

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                if (i < 4)
                {
                    popUps[i].SetActive(true);
                }
                else if (i == 4 && frogger.transform.position.y == 0)
                {
                    popUps[i].SetActive(true);
                }
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0) //Tutorial just started use left and right
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) 
                || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1) //Use up and down
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
                || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (waitTime2 <= 0)
            {
                popUpIndex++;
            }
            else
            {
                waitTime2 -= Time.deltaTime;
            }
        }
        else if (popUpIndex == 3) 
        {
            if (waitTime <= 0)
            {
                gameEnv.SetActive(true);
                popUpIndex++;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            
        }
        
        
    }

}
