using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject objectToSpawn;

    public void OnPointerDown(PointerEventData eventData)
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        if (GameManager.Instance.currentlyDraggedObject != null)
        {
            GameManager.Instance.currentlyDraggedObject = null;
        }

        print("SpawnButton: SpawnObject");

        GameObject spawnedObject = GameObject.Instantiate(objectToSpawn, (Vector2)Input.mousePosition, Quaternion.identity);
    }
}
