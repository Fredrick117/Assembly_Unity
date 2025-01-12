using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public delegate void SubmitDesign();
    public static SubmitDesign onSubmit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void OnSubmitClicked()
    {
        onSubmit?.Invoke();
    }
}
