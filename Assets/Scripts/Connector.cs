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
    public ConnectorType type;

    public ShipModule connectedModule = null;

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

        // Update is called once per frame
        void Update()
    {
        
    }

    //private void OnValidate()
    //{
    //    switch (type)
    //    {
    //        case ConnectorType.Type1:
    //            spriteRenderer.color = Color.red;
    //            break;

    //        case ConnectorType.Type2:
    //            spriteRenderer.color = Color.green;
    //            break;

    //        case ConnectorType.Type3:
    //            spriteRenderer.color = Color.blue;
    //            break;

    //        default:
    //            break;
    //    }
    //}

    public void ConnectToModule(ShipModule newModule)
    {
        connectedModule = newModule;
    }
}
