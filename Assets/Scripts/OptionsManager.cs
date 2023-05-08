using UnityEngine;
using TMPro;
using System;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro undoBtn;
    private string waitForInput = "...";


    private void Start()
    {
        if (PlayerPrefs.GetString("Undo") == "")
        {
            SetDefault();
        }
        else
        {
            undoBtn.text = PlayerPrefs.GetString("Undo");
        }
    }

    private void Update()
    {
        if (undoBtn.text == waitForInput)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keycode))
                {
                    undoBtn.text = keycode.ToString();
                    PlayerPrefs.SetString("Undo", keycode.ToString());
                }
            }
        }
    }

    public void ChangeUndoKey()
    {
        undoBtn.text = waitForInput;
    }

    public void SetDefault()
    {
        PlayerPrefs.SetString("Undo", "U");
    }
}
