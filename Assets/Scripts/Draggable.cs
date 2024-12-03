using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;

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

    private void Awake()
    {

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
        // Send message to DraggableManager, see if this can be dragged

        if (this.isDragging)
        {
            print("place object");
            PlaceObject();
        }
        else
        {
            print("begin dragging");
            BeginDragging();
        }
    }

    private void SetCanPlaceModule()
    {
        if (ShipManager.Instance.rootModule == null || ShipManager.Instance.rootModule == this.gameObject)
        {
            canPlace = true;
            return;
        }

        if (shipModule.IsColliding())
        {
            Debug.LogError("Cannot place, is colliding!");
            canPlace = false;
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

    public void BeginDragging()
    {
        print("begin dragging");
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

        if (ShipManager.Instance.rootModule == null)
        {
            print("I am the root!");
            ShipManager.Instance.rootModule = gameObject;
        }

        // TODO: move this to ShipModule class!
        hullSprite.color = Color.white;
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
