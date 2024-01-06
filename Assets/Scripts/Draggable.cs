using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    // private Transform[] connectorsArray;
    
    [SerializeField]
    private float maxDistanceFromCenter = 1f;
    private float dragSpeed = 0.2f;

    private Rigidbody2D rb;

    private void Start()
    {
        // connectorsArray = gameObject.transform.GetComponentsInChildren<Transform>();
        // connectorsArray = connectorsArray.Where(child => child.tag == "Connector").ToArray();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            rb.angularVelocity = 0f;
            print("E up!");
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            rb.angularVelocity = 0f;
            print("Q up!");
        }

        if (isDragging)
        {
            // Set velocity to last direction
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            direction = direction.normalized;

            float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.position);

            rb.velocity = direction * (distance * dragSpeed);
        }
    }

    private void OnMouseOver()
    {
        // Rotate the module with Q and E
        // TODO: update this to new input system
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("e");
            rb.angularVelocity = -50f;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            print("q");
            rb.angularVelocity = 50f;
        }
    }

    private void OnMouseDown()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        isDragging = true;

        // for (int i = 0; i < connectorsArray.Length; i++)
        // {
        //     ModuleConnection connection = connectorsArray[i].gameObject.GetComponent<ModuleConnection>();
        //     if (connection.LinkedConnector != null)
        //     {
        //         connection.IsOccupied = false;
        //         connection.LinkedConnector.GetComponent<ModuleConnection>().IsOccupied = false;
        //         connection.LinkedConnector.GetComponent<ModuleConnection>().LinkedConnector = null;
        //         connection.LinkedConnector = null;
        //     }
        // }
    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseDistance = Vector2.Distance(gameObject.transform.position, mousePosition);

        if (mouseDistance < 0.1f)
        {
            //print(mouseDistance);
            rb.velocity = Vector2.zero;
        }

        isDragging = false;

        // float closestConnectorDistance = 99999.0f;
        // GameObject closestOtherConnector = null;
        // GameObject closestChildConnector = null;

        // // Check if any of this object's connectors are close to another object's connector
        // for (int i = 0; i < connectorsArray.Length; i++)
        // {
        //     Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);

        //     if (hitColliders.Length == 0)
        //         continue;

        //     foreach (var hit in hitColliders)
        //     {
        //         float connectorDistance = Vector2.Distance(connectorsArray[i].transform.position, hit.gameObject.transform.position);

        //         if (hit.gameObject.CompareTag("Connector")
        //         && connectorDistance < closestConnectorDistance
        //         && hit.gameObject.transform.parent != gameObject.transform)
        //         {
        //             closestConnectorDistance = connectorDistance;
        //             closestOtherConnector = hit.gameObject;
        //             closestChildConnector = connectorsArray[i].gameObject;
        //         }
        //     }
        // }

        // if (closestChildConnector != null && closestOtherConnector != null && !closestOtherConnector.GetComponent<ModuleConnection>().IsOccupied)
        // {
        //     transform.position = closestOtherConnector.transform.position - closestChildConnector.transform.localPosition;
        //     closestOtherConnector.GetComponent<ModuleConnection>().IsOccupied = true;
        //     closestChildConnector.GetComponent<ModuleConnection>().IsOccupied = true;
        //     closestChildConnector.GetComponent<ModuleConnection>().LinkedConnector = closestOtherConnector;
        //     closestChildConnector.GetComponent<ModuleConnection>().LinkedConnector = closestChildConnector;
        // }
    }
}
