using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnCloseClicked(GameObject uiElement)
    {
        uiElement.SetActive(false);
    }
}
