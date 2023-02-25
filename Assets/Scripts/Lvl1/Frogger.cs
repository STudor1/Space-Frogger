using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite leapSprite;

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

        StartCoroutine(Leap(destination));
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

}
