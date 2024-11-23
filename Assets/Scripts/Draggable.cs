using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    public bool isDragging = false;

    private Vector3 offset;

    private Vector3 initialPickupPosition = Vector3.negativeInfinity;

    private Rigidbody2D rb;

    public float snapDistance = 0.025f;

    [SerializeField]
    private Color validPlacementColor = Color.green;

    [SerializeField]
    private Color invalidPlacementColor = Color.red;

    [SerializeField]
    private SpriteRenderer hullSprite;

    private ShipModule shipModule;

    private bool canPlace = false;

    private DraggableManager draggableManager;

    private void Awake()
    {
        draggableManager = GameObject.Find("DraggableManager").GetComponent<DraggableManager>();
        draggableManager.BeginDraggingObject(this);
    }

    private void Start()
    {
        shipModule = GetComponent<ShipModule>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void SetRoundedPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 roundedPosition = new Vector3(Mathf.Round(mousePosition.x * 2f) / 2f, Mathf.Round(mousePosition.y * 2f) / 2f, mousePosition.z);

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
        if (ShipManager.Instance.rootModule == null)
        {
            canPlace = true;
            return;
        }

        if (shipModule.IsColliding())
        {
            print("Cannot place, is colliding!");
            canPlace = false;
            return;
        }

        // Check if colliding with valid components
        foreach (Connector connector in shipModule.connectors)
        {
            print("connector: " + connector.name);
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

        draggableManager.PlaceDraggable(gameObject);

        // TODO: move this to ShipModule class!
        hullSprite.color = Color.white;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("dragging!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("pointer down!");
    }



    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;

    //    foreach (Connector connector in shipModule.connectors)
    //    {
    //        Gizmos.DrawSphere(connector.transform.position, 0.1f);
    //    }
    //}
}
