using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ConnectorType
{
    Type1,
    Type2,
    Type3,
};

public class Connector : MonoBehaviour
{
    private bool isOccupied = false;

    public ConnectorType type;

    public GameObject connectedObject = null;

    public Color connectorColor = Color.black;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        switch (type)
        {
            case ConnectorType.Type1:
                spriteRenderer.color = Color.red;
                break;

            case ConnectorType.Type2:
                spriteRenderer.color = Color.green;
                break;

            case ConnectorType.Type3:
                spriteRenderer.color = Color.blue;
                break;

            default:
                break;
        }
    }

    public void Connect(bool isOccupied)
    {
        if (this.isOccupied != isOccupied)
            this.isOccupied = isOccupied;
    }

    public void SetConnectedObject(GameObject newObject)
    {
        connectedObject = newObject;
    }
}
