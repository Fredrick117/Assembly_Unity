using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;

    private Transform[] connectorsArray;

    private Vector3 initialPickupPosition;

    private bool isOver;

    private Rigidbody2D rb;

    public float snapDistance = 0.025f;

    [SerializeField]
    private Color validPlacementColor = Color.green;
    [SerializeField]
    private Color invalidPlacementColor = Color.red;

    [SerializeField]
    private SpriteRenderer hullSprite;

    private bool isCollidingWithOtherObject = false;

    private void Start()
    {
        connectorsArray = gameObject.transform.GetComponentsInChildren<Transform>();
        connectorsArray = connectorsArray.Where(child => child.tag == "Connector").ToArray();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 roundedPosition = new Vector3(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y), mousePosition.z);
            rb.position = roundedPosition;

            hullSprite.color = CanPlace() ? validPlacementColor : invalidPlacementColor;
        }
    }

    private bool HasValidConnectorNearby()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), snapDistance);
        
        foreach (Collider2D col in collisions)
        {
            if (col.gameObject.tag == "Connector")
                return true;
        }

        return false;
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        initialPickupPosition = transform.position;

        //for (int i = 0; i < ConnectorsArray.Length; i++)
        //{
        //    ModuleConnection Connection = ConnectorsArray[i].gameObject.GetComponent<ModuleConnection>();
        //    if (Connection.LinkedConnector != null)
        //    {
        //        Connection.IsOccupied = false;
        //        Connection.LinkedConnector.GetComponent<ModuleConnection>().IsOccupied = false;
        //        Connection.LinkedConnector.GetComponent<ModuleConnection>().LinkedConnector = null;
        //        Connection.LinkedConnector = null;
        //    }
        //}
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (!CanPlace())
        {
            transform.position = initialPickupPosition;
        }
        else
        {
            // TODO: placement
        }

        hullSprite.color = Color.white;
    }

    private bool CanPlace()
    {
        return !isCollidingWithOtherObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollidingWithOtherObject = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollidingWithOtherObject = false;
    }
}
