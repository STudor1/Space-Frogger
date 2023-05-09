using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class Frogger : MonoBehaviour, IEntity
{
    private IPlayerState currentState;
    [SerializeField] private Frogger frogger;
    private GameManager gameManager;
    private InputHandler inputHandler;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite idleSprite;
    [SerializeField] private Sprite leapSprite;
    [SerializeField] private Sprite deadSprite;

    private Vector3 spawnPosition;
    public float farthestRow;
    private bool isPaused;
    private int deathCount;
    private int selectedProfile;
    private AchievementService achievementSystem;
    private SaveData data;
    UserProfile currentUser;

    //public static UnityEvent<Achievement> ach;

    private void Awake()
    {
        selectedProfile = int.Parse(ProfileManagerJson.selectedProfile);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        inputHandler = GetComponent<InputHandler>();
        gameManager = FindObjectOfType<GameManager>();
        currentState = new PlayerIdle();
        achievementSystem = FindObjectOfType<AchievementService>();

        //loading in the current player
        

    }

    private void Update()
    {
        //isPaused = FindObjectOfType<GameManager>().IsPaused();

        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.U)) //undo
            {
                inputHandler.UndoCommand();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                KeyCode input = KeyCode.W;
                UpdateState(input);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                KeyCode input = KeyCode.S;
                UpdateState(input);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                KeyCode input = KeyCode.A;
                UpdateState(input);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                KeyCode input = KeyCode.D;
                UpdateState(input);
            }
        }

        
    }

    //Updates the current state frogger is in
    private void UpdateState(KeyCode input)
    {
        IPlayerState newState = currentState.Tick(this.transform, input, frogger, gameManager, farthestRow, inputHandler);

        if (newState != null)
        {

            currentState.Exit();
            currentState = newState;
            newState.Enter();

            if (currentState.ToString() == "PlayerMoving")
            {
                newState = currentState.Tick(this.transform, input, frogger, gameManager, farthestRow, inputHandler);
                currentState.Exit();
                currentState = newState;
                newState.Enter();
            }

        }
    }

    //This roatates Frogger in the direction based on the key pressed
    public void Rotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    //We are leaping to the destination passed in
    public IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position; //this grabs our current position
        float elapsedTime = 0f;
        float duration = 0.125f; //this is how long the animation lasts

        spriteRenderer.sprite = leapSprite;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsedTime += Time.deltaTime; //Time.deltaTime indicates how much time has passed since last frame to the current one
            yield return null; //this pauses the execution of Leap and waits until the next frame
        }

        transform.position = destination; //makes sure frogger is at the correct position
        spriteRenderer.sprite = idleSprite;
    }


    public void Death()
    {
        
        StopAllCoroutines();

        transform.rotation = Quaternion.identity; //resets rotation, identity is like 0
        spriteRenderer.sprite = deadSprite;
        enabled = false; //disables this so you can control frogger when you are dead

        //Invoke(nameof(Respawn), 1f); //this calls respawn after 1 second

        FindObjectOfType<GameManager>().Died();
    }

    public void Respawn()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        farthestRow = spawnPosition.y;
        spriteRenderer.sprite = idleSprite;
        gameObject.SetActive(true);
        enabled = true;
        
    }

    //This gets called when a collision has happened between our object and another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we first make sure our script is enabled(frogger is alive) and the layer
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null) {
            Debug.Log("Frogger is dead");

            Death();
        }
    }

    public void IsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
