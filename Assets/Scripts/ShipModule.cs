using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipModule : MonoBehaviour
{
    public bool isConnectedToRoot = false;

    public List<GameObject> connectedModules = new List<GameObject>();

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnModulePlaced(GameObject placedObject)
    {
        print("yipee!");
    }
}
