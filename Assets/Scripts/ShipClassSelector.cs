using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipClassSelector : MonoBehaviour
{
    public void OnSelectShipClass(GameObject shipToSpawn)
    {
        if (ShipManager.Instance.currentShip != null)
            GameObject.Destroy(ShipManager.Instance.currentShip);

        GameObject spawnedShip = GameObject.Instantiate(shipToSpawn, Vector3.zero, Quaternion.identity);

        ShipManager.Instance.SetShip(spawnedShip);

        gameObject.SetActive(false);
    }
}
