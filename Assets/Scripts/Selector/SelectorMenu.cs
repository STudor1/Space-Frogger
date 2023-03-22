using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMenu : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
