using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ConnectorType
{
    Module,
    Engine,
    Weapon,
};

public class Connector : MonoBehaviour
{
    public ConnectorType type;

    //public GameObject connectedObject = null;
    public Connector otherConnector = null;

    public Color connectorColor = Color.black;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        switch (type)
        {
            case ConnectorType.Module:
                spriteRenderer.color = Color.red;
                break;

            case ConnectorType.Engine:
                spriteRenderer.color = Color.green;
                break;

            case ConnectorType.Weapon:
                spriteRenderer.color = Color.blue;
                break;

            default:
                break;
        }
    }
}
