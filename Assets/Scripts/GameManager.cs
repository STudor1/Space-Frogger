using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;

    private Home[] homes;
    private Frogger frogger;
    private int score;
    private int lives;
    private int time;

    private void Awake()
    {
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }

    private void Start()
    {
        NewGame();
    }

    //Start fresh, 0 score, intial lives
    private void NewGame()
    {
        gameOverMenu.SetActive(false);

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

        Respawn();
    }

    private void Respawn()
    {
        frogger.Respawn();

        StopAllCoroutines();
        StartCoroutine(Timer(30));
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
        }

        frogger.Death(); // if we didn't make it in time frogger dies
    }

    public void Died()
    {
        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f);
        }
        else
        {
            Invoke(nameof(GameOver), 1f);
        }
    }

    private void GameOver()
    {
        frogger.gameObject.SetActive(false); //turn frogger off
        gameOverMenu.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }

    //We need this coroutine do constantly check for input
    private IEnumerator PlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }

            yield return null;
        }

        NewGame();
    }

    public void AdvancedRow()
    {
        SetScore(score + 10);
    }

    public void HomeOccupied()
    {
        frogger.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        SetScore(score + bonusPoints + 50 );

        if (LevelCleared())
        {
            SetScore(score + 1000);
            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            Invoke(nameof(Respawn), 1f);
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
