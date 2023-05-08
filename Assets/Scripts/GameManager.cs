using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnHomeEnter; //takes in an int - seconds left on counter
    public static event Action<int> OnLevelFinish; //takes in an int - lives left
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pausedMenu;
    //[SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private UnityEvent<bool> onPause;

    public static event Action<int> OnDeath; //takes in an int - total death count
    private Home[] homes;
    private Frogger frogger;
    private Lvl1MoveCycle[] gameEnvironment;
    private int score;
    private int lives;
    private int time;
    private bool isPaused = false;
    private bool isFrogActive;
    private int selectedProfile;
    private SaveData data;
    //private int time = 0;
    UserProfile currentUser;
    private int deathCount;


    private void Awake()
    {
        selectedProfile = int.Parse(ProfileManagerJson.selectedProfile);
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
        gameEnvironment = FindObjectsOfType<Lvl1MoveCycle>();
        data = FindObjectOfType<SaveData>();

        //loading in the current player
        currentUser = data.LoadProfile(selectedProfile);
        Debug.Log("Playing as " + currentUser.username);
    }

    private void Start()
    {
        NewGame();
        InvokeRepeating("TickTimer", 0f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Debug.Log("Game paused is " + isPaused);
            PauseGame(isPaused);
        }

        //Invoke();
        //TickTimer();
    }

    private void PauseGame(bool _isPaused)
    {
        pausedMenu.SetActive(_isPaused);
        onPause?.Invoke(_isPaused);
        for (int i = 0; i < gameEnvironment.Length; i++)
        {
            gameEnvironment[i].SendMessage("IsPaused", _isPaused);
        }

        
    }

    //Start fresh, 0 score, intial lives
    private void NewGame()
    {
        gameOverMenu.SetActive(false);
        pausedMenu.SetActive(false);

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
            homes[i].enabled = false; //access all homes and set the state to un occupied 
        }

        Respawn();
    }

    private void Respawn()
    {
        frogger.Respawn();
        isFrogActive = true;

        StopAllCoroutines();
        StartTimer(30);
        //timer.startTimer(30);
        //StartCoroutine(Timer(30));
    }

    public void Died()
    {
        SetLives(lives - 1);

        deathCount = currentUser.deathCount;
        deathCount++;
        currentUser.deathCount = deathCount;
        data.SaveTheData(currentUser, currentUser.id);

        OnDeath?.Invoke(deathCount);


        if (lives > 0)
        {
            isFrogActive = false;
            Invoke(nameof(Respawn), 1f);
        }
        else
        {
            isFrogActive = false;
            Invoke(nameof(GameOver), 1f);
        }
    }

    private void GameOver()
    {
        frogger.gameObject.SetActive(false); //turn frogger off
        gameOverMenu.SetActive(true);

        string name = currentUser.username;
        int highscore = currentUser.highscore;

        //if the current highscore is smaller than the current score, update the highscore
        if (highscore < score)
        {
            currentUser.highscore = score;
            data.SaveTheData(currentUser, currentUser.id);
            Debug.Log("New highscore for " + name + ": " + score);
        }
        else
        {
            Debug.Log("Current highscore for " + name + " is " + highscore);
        }
        

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
            isFrogActive = false;
            SetScore(score + 1000);
            OnLevelFinish?.Invoke(lives);
            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            isFrogActive = false;
            OnHomeEnter?.Invoke(time);
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
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    private void StartTimer(int duration)
    {
        time = duration;
        timeText.text = time.ToString();
        Debug.Log("Time set to " + duration);
    }

    private void TickTimer()
    {
        //Debug.Log("Tick Time " + isFrogActive);
        timeText.text = time.ToString();

        if (time == 9999)
        {
            timeText.text = "0";
            time = 9999;
        } 
        else if (time > 0 && !isPaused && isFrogActive)
        {
            time--;
            //Debug.Log("a " + time);

            timeText.text = time.ToString();
        }
        
        if(time == 0 && !isPaused)
        {
            time = 9999;
            frogger.Death();
        }

        
    }

    public void PausedPress()
    {
        isPaused = !isPaused;
        PauseGame(isPaused);
        Debug.Log("Pressed it");
    }

    public void HomePress()
    {
        GameOver(); //to get the highscore and save it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
