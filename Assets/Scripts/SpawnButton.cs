using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject objectToSpawn;

    private int test;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.currentlyDraggedObject != null)
        {
            GameManager.Instance.currentlyDraggedObject = null;
        }

        GameObject spawnedModule = GameObject.Instantiate(objectToSpawn, (Vector2)Input.mousePosition, Quaternion.identity);
        spawnedModule.name = spawnedModule.name + " " + test;
        test++;
    }
}
