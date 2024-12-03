using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    [HideInInspector]
    public Draggable currentlyDraggedObject = null;

    public static DraggableManager Instance { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
