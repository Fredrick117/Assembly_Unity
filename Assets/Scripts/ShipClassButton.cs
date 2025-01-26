using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipClassButton : MonoBehaviour
{
    [SerializeField]
    private GameObject militaryClasses;
    
    [SerializeField]
    private GameObject civilianClasses;

    public void OnClassButtonClicked()
    {
        if (ShipManager.Instance.currentShipType == ShipType.Military)
        {
            militaryClasses.SetActive(true);
        }
        else if (ShipManager.Instance.currentShipType == ShipType.Civilian)
        {
            civilianClasses.SetActive(true);
        }
    }
}
