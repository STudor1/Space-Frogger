using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait for it to end
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(sceneIndex);

    }
}
