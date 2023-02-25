using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{
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
        transform.position += direction;
    }

    private void Rotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
