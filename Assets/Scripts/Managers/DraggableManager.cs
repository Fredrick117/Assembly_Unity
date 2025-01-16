using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    [HideInInspector]
    public Draggable currentlyDraggedObject = null;

    public static DraggableManager Instance { get; private set; }

    public delegate void PlaceModule();
    public static PlaceModule onPlace;

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

    public void DraggablePlaced()
    {
        onPlace?.Invoke();
    }
}
