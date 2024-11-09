using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string startScene = "TestScene";

    public void OnStart()
    {
        SceneManager.LoadScene(startScene);
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
