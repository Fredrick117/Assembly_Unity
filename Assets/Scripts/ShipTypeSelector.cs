using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipTypeSelector : MonoBehaviour
{
    [SerializeField]
    private Button shipClassButton;

    public void OnSelectShipType(int type)
    {
        if (type == 0)
        {
            ShipManager.Instance.currentShipType = ShipType.Military;
        }
        else
        {
            ShipManager.Instance.currentShipType = ShipType.Civilian;
        }

        gameObject.SetActive(false);
        shipClassButton.interactable = true;
    }
}
