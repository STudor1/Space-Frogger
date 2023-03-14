using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject frogInHome;

    //When our home is occupied
    private void OnEnable()
    {
        frogInHome.SetActive(true);
    }

    private void OnDisable()
    {
        frogInHome.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enabled = true;

            //Frogger frogger = collision.GetComponent<Frogger>(); //this gets the component of the item the collided with the home i.e frogger
            //frogger.gameObject.SetActive(false);
            //frogger.Invoke(nameof(frogger.Respawn), 1f);

            FindObjectOfType<GameManager>().HomeOccupied(); // we inform the game manager that the home has been occupied
        }
    }
}
