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

        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject spawnedObject = GameObject.Instantiate(prefabToSpawn, spawnPosition, prefabToSpawn.transform.rotation);
        spawnedObject.GetComponent<Draggable>().isDragging = true;
        GameManager.Instance.isDragging = true;
        GameManager.Instance.draggedObject = spawnedObject;
    }
}
