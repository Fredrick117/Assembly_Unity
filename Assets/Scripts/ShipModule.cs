using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipModule : MonoBehaviour
{
    public bool isConnectedToRoot = false;

    public List<ShipModule> connectedModules = new List<ShipModule>();

    public Connector[] connectors;

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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
