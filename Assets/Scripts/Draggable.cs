using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    public bool isDragging = false;

    private Vector3 offset;

    private Vector3 initialPickupPosition = Vector3.negativeInfinity;

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

    ShipModule shipModule;

    private bool canPlace = false;

    private void Awake()
    {
        GameManager.Instance.BeginDraggingObject(gameObject);
        isDragging = true;
    }

    private void Start()
    {
        shipModule = GetComponentInChildren<ShipModule>();
        shipModule.connectors = gameObject.transform.GetComponentsInChildren<Connector>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void SetRoundedPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 roundedPosition = new Vector3(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y), mousePosition.z);

        if (rb.position != (Vector2)roundedPosition)
        {
            rb.position = roundedPosition;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            SetRoundedPosition();
            SetCanPlaceModule();
            OnPositionChanged();
        }
    }

    private void OnPositionChanged()
    {
        hullSprite.color = canPlace ? validPlacementColor : invalidPlacementColor;
    }

    private void OnMouseDown()
    {
        if (isDragging)
        {
            PlaceObject();
        }
        else
        {
            BeginDragging();
        }
    }

    private void SetCanPlaceModule()
    {
        // Only works on initial spawn
        if (ShipManager.Instance.rootModule == null)
        {
            canPlace = true;
            return;
        }

        // Check if colliding with valid components
        foreach (Connector connector in shipModule.connectors)
        {
            Collider2D[] nearbyConnectors = Physics2D.OverlapCircleAll(connector.transform.position, 0.1f, LayerMask.GetMask("Connector"));

            foreach (Collider2D hit in nearbyConnectors)
            {
                Connector nearbyConnector = hit.GetComponent<Connector>();

                if (nearbyConnector == null || nearbyConnector == connector || nearbyConnector.transform.IsChildOf(transform))
                {
                    continue;
                }

                // TODO: what if there's more than one connector?
                if (connector.type == nearbyConnector.type)
                {
                    canPlace = true;
                    return;
                }
            }
        }

        canPlace = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollidingWithOtherObject = false;
    }

    public void BeginDragging()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        initialPickupPosition = transform.position;
    }

    public void PlaceObject()
    {
        isDragging = false;

        if (!canPlace)
        {
            if (Vector3.Equals(initialPickupPosition, Vector3.negativeInfinity))
            {
                Destroy(gameObject);
                return;
            }

            transform.position = initialPickupPosition;
        }

        GameManager.Instance.HandleDraggablePlaced(gameObject);

        // TODO: move this to ShipModule class!
        hullSprite.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (Connector connector in shipModule.connectors)
        {
            Gizmos.DrawSphere(connector.transform.position, 0.1f);
        }
    }
}
