using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Fabricator fabricator;

    public void SpawnPrefab()
    {
        print("button pressed!");

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPosition = new Vector3(mousePosition.x, mousePosition.y, prefabToSpawn.transform.position.z);
        GameObject spawnedObject = GameObject.Instantiate(prefabToSpawn, spawnPosition, prefabToSpawn.transform.rotation);
        spawnedObject.GetComponent<Draggable>().isDragging = true;
        GameManager.Instance.isDragging = true;
        GameManager.Instance.draggedObject = spawnedObject;
    }
}
