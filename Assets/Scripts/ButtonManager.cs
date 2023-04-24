using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSubmit()
    {
        EventManager.Instance.OnSubmitClicked();
    }
}
