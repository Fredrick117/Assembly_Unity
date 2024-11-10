using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    [HideInInspector]
    public Draggable currentlyDraggedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceDraggable(GameObject draggedObject)
    {
        if (ShipManager.Instance.rootModule == null)
        {
            print("I am the root");
            ShipManager.Instance.rootModule = draggedObject;
        }
    }

    public void BeginDraggingObject(Draggable draggedObject)
    {
        if (!currentlyDraggedObject)
        {
            currentlyDraggedObject = draggedObject;
        }
    }
}
