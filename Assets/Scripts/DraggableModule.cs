using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DraggableModule : MonoBehaviour
{
    private bool IsDragging = false;
    private Vector3 Offset;

    private Transform[] ConnectorsArray;

    private Vector2 grabPosition;

    private Rigidbody2D rb;

    private bool isOver;

    private void Start()
    {
        ConnectorsArray = gameObject.transform.GetComponentsInChildren<Transform>();
        ConnectorsArray = ConnectorsArray.Where(child => child.tag == "Connector").ToArray();

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

        if (IsDragging)
        {
            // Set velocity to last direction
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;

            rb.velocity = direction * 2f;
        }
    }

    private void OnMouseOver()
    {
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

        Offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsDragging = true;
        grabPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < ConnectorsArray.Length; i++)
        {
            ModuleConnection Connection = ConnectorsArray[i].gameObject.GetComponent<ModuleConnection>();
            if (Connection.LinkedConnector != null)
            {
                Connection.IsOccupied = false;
                Connection.LinkedConnector.GetComponent<ModuleConnection>().IsOccupied = false;
                Connection.LinkedConnector.GetComponent<ModuleConnection>().LinkedConnector = null;
                Connection.LinkedConnector = null;
            }
        }
    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(gameObject.transform.position, mousePosition);
        if (distance < 0.1f)    // ?
        {
            print(distance);
            rb.velocity = Vector2.zero;
            //rb.angularVelocity = 0f;
        }

        IsDragging = false;
        float ClosestConnectorDistance = 99999.0f;
        GameObject ClosestOtherConnector = null;
        GameObject ClosestChildConnector = null;

        // Check if any of this object's connectors are close to another object's connector
        for (int i = 0; i < ConnectorsArray.Length; i++)
        {
            Collider2D[] HitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);

            if (HitColliders.Length == 0)
                continue;

            foreach (var Hit in HitColliders)
            {
                float Distance = Vector2.Distance(ConnectorsArray[i].transform.position, Hit.gameObject.transform.position);

                if (Hit.gameObject.tag == "Connector"
                && Distance < ClosestConnectorDistance
                && Hit.gameObject.transform.parent != gameObject.transform)
                {
                    ClosestConnectorDistance = Distance;
                    ClosestOtherConnector = Hit.gameObject;
                    ClosestChildConnector = ConnectorsArray[i].gameObject;
                }
            }
        }

        if (ClosestChildConnector != null && ClosestOtherConnector != null && !ClosestOtherConnector.GetComponent<ModuleConnection>().IsOccupied)
        {
            transform.position = ClosestOtherConnector.transform.position - ClosestChildConnector.transform.localPosition;
            ClosestOtherConnector.GetComponent<ModuleConnection>().IsOccupied = true;
            ClosestChildConnector.GetComponent<ModuleConnection>().IsOccupied = true;
            ClosestChildConnector.GetComponent<ModuleConnection>().LinkedConnector = ClosestOtherConnector;
            ClosestOtherConnector.GetComponent<ModuleConnection>().LinkedConnector = ClosestChildConnector;
        }
    }
}
