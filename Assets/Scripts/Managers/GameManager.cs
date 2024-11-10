using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private DraggableManager draggableManager;
    [SerializeField]
    private ShipRequestManager shipRequestManager;
    [SerializeField]
    private ShipManager shipManager;

    public GameObject moneyCounter;

    // TODO: implement
    public bool holdToDrag = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SpawnObjectFromButton(GameObject newObject)
    {
        GameObject spawnedObject = GameObject.Instantiate(newObject, (Vector2)Input.mousePosition, Quaternion.identity);

        if (spawnedObject != null)
        {
            // TODO: what if object doesn't have Draggable component?
            draggableManager.currentlyDraggedObject = spawnedObject.GetComponent<Draggable>();
            spawnedObject.GetComponent<Draggable>().isDragging = true;
        }
        else
        {
            print("SpawnedObject was null!");
        }
    }
}
