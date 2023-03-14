using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Home[] homes;
    private Frogger frogger;
    private int score;
    private int lives;

    private void Awake()
    {
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }


    //Start fresh, 0 score, intial lives
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    //This happens after you occupy all homes, start new lvl, homes become empty, continue getting score
    //everytime you clear a lvl you get x amount of points added to the score
    private void NewLevel()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            homes[i].enabled = false; //acces all homes and set the state to un occupied 
        }

        NewRound();
    }

    //Every time you occupy one home, you are starting a new round
    private void NewRound()
    {
        frogger.Respawn();
    }

    public void HomeOccupied()
    {
        frogger.gameObject.SetActive(false);

        if (LevelCleared())
        {
            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            Invoke(nameof(NewRound), 1f);
        }
    }

    private bool LevelCleared()
    {

        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled)
            {
                return false; //check to see if all homes are occupied, if it find one that isn't it returns false
            }
        }

        return true; //if we make it thru' the loop then that must mean that all homes are occupied and return true

    }

    private void SetScore(int score)
    {
        this.score = score;
        //update ui
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        //update ui
    }

}
