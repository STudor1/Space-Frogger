using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorMenu : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void GoBackToProfiles()
    {
        SceneManager.LoadScene("LvlSelector");
    }
}
