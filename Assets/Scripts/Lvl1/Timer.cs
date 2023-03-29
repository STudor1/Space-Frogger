using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private bool isOn = false;

    private void Update()
    {
        if (isOn)
        {
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                Debug.Log("Time left: " + currentTime);
            }
            Debug.Log("Times up!");
            isOn = false;
        }

    }

    public void startTimer(int duration)
    {
        Debug.Log("Starting " + duration + " timer");
        currentTime = duration;
        isOn = true;
        Update();
    }

}
