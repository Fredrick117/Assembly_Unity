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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToModule(ShipModule newModule)
    {
        connectedModule = newModule;
    }
}
