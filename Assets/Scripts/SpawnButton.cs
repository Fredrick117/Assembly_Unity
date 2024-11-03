using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject objectToSpawn;

    private TMP_Text buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();

        if (objectToSpawn != null)
        {
            buttonText.text = objectToSpawn.name;
        }
        else
        {
            buttonText.text = "null";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.currentlyDraggedObject != null)
        {
            GameManager.Instance.currentlyDraggedObject = null;
        }

        GameObject spawnedModule = GameObject.Instantiate(objectToSpawn, (Vector2)Input.mousePosition, Quaternion.identity);
    }
}
