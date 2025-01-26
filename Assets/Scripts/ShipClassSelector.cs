using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipClassSelector : MonoBehaviour
{
    public void OnSelectShipClass(GameObject shipToSpawn)
    {
        GameObject.Instantiate(shipToSpawn, Vector3.zero, Quaternion.identity);

        ShipManager.Instance.SetShip(shipToSpawn);

        gameObject.SetActive(false);
    }
}
