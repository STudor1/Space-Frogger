using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1MoveCycle : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private bool isPaused;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); // left edge is 0 0
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right); //right edge is 1 1
    }

    private void Update()
    {
        //isPaused = FindObjectOfType<GameManager>().IsPaused();

        if (!isPaused)
        {
            //if > 0 moving to the right therefore check right edge
            //taking the position 0 0 of the object and taking away the size since object can have different sizes
            if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
            {
                Vector3 position = transform.position;
                position.x = leftEdge.x - size;
                transform.position = position;
            }
            else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
            {
                Vector3 position = transform.position;
                position.x = rightEdge.x + size;
                transform.position = position;
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        
    }

    public void IsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
