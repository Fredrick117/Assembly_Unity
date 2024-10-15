using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectorType
{
    ENGINE,
    ROOM,
    WEAPON
}

public class ModuleConnection : MonoBehaviour
{
    [HideInInspector]
    public bool IsOccupied;

    public ConnectorType Type;

    public Sprite ConnectorSprite;

    public GameObject LinkedConnector = null;

    public bool left = true;

    public Draggable parentModule;

    private void Start()
    {
        parentModule = gameObject.transform.parent.GetComponent<Draggable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //parentModule.OnConnectorCollision(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("I have collided!");
        //if (parentModule.isDragging)
        //{
        //    transform.parent.position = collision.gameObject.transform.parent.GetComponent<Draggable>().transform.position;
        //}
    }

    private void Update()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //if (hit.collider != null)
        //{
        //    if (hit.collider.gameObject == this.gameObject)
        //    {
        //        Debug.Log("Mouse is over the child collider!");
        //    }
        //}
    }
}
