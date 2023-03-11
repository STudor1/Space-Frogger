using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite leapSprite;
    [SerializeField] private Sprite deadSprite;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Rotation(0);
            Move(Vector3.up);
        } 
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            Rotation(180);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotation(90);
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotation(-90);
            Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        //This means that if there is a barrier on the next destination we return without doing the code after
        if (barrier != null)
        {
            return;
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform); //attach our self to platform
        } else
        {
            transform.SetParent(null); //detach from platform
        }

        if (obstacle != null && platform == null)
        {
            transform.position = destination; 
            Death();
        } else
        {
            StartCoroutine(Leap(destination));
        }
        
        //transform.position += direction;
    }
    
    //This roatates Frogger in the direction based on the key pressed
    private void Rotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    //We are leaping to the destination passed in
    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position; //this grabs our current position
        float elapsedTime = 0f;
        float duration = 0.125f; //this is how long the animation lasts

        spriteRenderer.sprite = leapSprite;

        while(elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsedTime += Time.deltaTime; //Time.deltaTime indicates how much time has passed since last frame to the current one
            yield return null; //this pauses the execution of Leap and waits until the next frame
        }

        transform.position = destination; //makes sure frogger is at the correct position
        spriteRenderer.sprite = idleSprite;
    }

    private void Death()
    {
        transform.rotation = Quaternion.identity; //resets rotation, identity is like 0
        enabled = false; //disables this so you can control frogger when you are dead
        spriteRenderer.sprite = deadSprite;
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
}
