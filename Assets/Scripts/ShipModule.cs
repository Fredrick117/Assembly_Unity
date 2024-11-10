using System.Collections.Generic;
using UnityEngine;

public class ShipModule : MonoBehaviour
{
    [HideInInspector]
    public bool isConnectedToRoot = false;

    [HideInInspector]
    public bool isCollidingWithShipModule = false;

    [HideInInspector]
    public List<ShipModule> connectedModules = new List<ShipModule>();

    [HideInInspector]
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

    public bool IsColliding()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.85f, 0.85f), 0f);

        if (collisions.Length > 0)
        {
            foreach (Collider2D col in collisions)
            {
                if (col.gameObject.CompareTag("ShipModule") && col.gameObject != gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.gameObject.CompareTag("ShipModule"))
    //     {
    //         print("entering");
    //         isCollidingWithShipModule = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     if (collider.gameObject.CompareTag("ShipModule"))
    //     {
    //         isCollidingWithShipModule = false;
        
    //         print("leaving");
    //     }
    // }
}
