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
        }
    }
}
