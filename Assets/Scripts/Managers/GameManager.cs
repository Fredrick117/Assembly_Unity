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

    public void BeginDraggingObject(GameObject draggedObject)
    {
        if (!draggableManager.currentlyDraggedObject)
        {
            draggableManager.currentlyDraggedObject = draggedObject;
        }
    }

    public void HandleDraggablePlaced(GameObject draggedObject)
    {
        if (ShipManager.Instance.rootModule == null)
        {
            print("I am the root");
            ShipManager.Instance.rootModule = draggedObject;
        }
    }

    public void SpawnObjectFromButton(GameObject newObject)
    {
        GameObject spawnedObject = GameObject.Instantiate(newObject, (Vector2)Input.mousePosition, Quaternion.identity);

        if (spawnedObject != null)
        {
            draggableManager.currentlyDraggedObject = spawnedObject;
        }
        else
        {
            print("SpawnedObject was null!");
        }
    }
}
