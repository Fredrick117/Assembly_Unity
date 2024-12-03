using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Subsystem
{
    Reactor,
    Navigation,
    LifeSupport,
    Shields,
    AI,
    FTLDrive
}

[System.Serializable]
public enum ShipType
{
    Warship,
    Exploration,
    Cargo,
}

[System.Serializable]
public enum ShipClass
{ 
    Corvette,
    Destroyer,
    Carrier
}

[System.Serializable]
public struct RequestData
{
    public int budget;
    public float minSpeed;
    public float maxSpeed;
    public ShipType shipType;
    public ShipClass shipClass;
    public HashSet<Subsystem> requiredSubsystems;
}
