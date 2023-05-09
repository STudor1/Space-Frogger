using UnityEngine;
using UnityEngine.UI;

public class VolumeSave : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        float volumeValue = PlayerPrefs.GetFloat("Volume");

        if (volumeValue == 0)
        {
            SaveVolume();
        }
        else
        {
            LoadValues();
        }
    }

    public void SaveVolume()
    {
        float volumeValue = volumeSlider.value;

        PlayerPrefs.SetFloat("Volume", volumeValue);
        LoadValues();
    }

    public void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
