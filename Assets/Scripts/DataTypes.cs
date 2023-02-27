using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ShipSubsystemType
{
    // THRUSTER,
    REACTOR,
    // MEDBAY,
    // NAVIGATION,
    // WEAPON,
    LIFE_SUPPORT,
    // REPAIR,
    SHIELDS,
    // HANGAR
}

[System.Serializable]
public class ShipSubsystem
{
    public string Name;
    public ShipSubsystemType Type;
}

[System.Serializable]
public class ReactorSubsystemData : ShipSubsystem
{
    // Maximum power
    public float Power;

    // Maximum number of engines this reactor can fully power
    public int MaxEngines;

    public ReactorSubsystemData(string _name, ShipSubsystemType _type, float _power, int _maxEngines)
    {
        Name = _name;
        Type = _type;
        Power = _power;
        MaxEngines = _maxEngines;
    }
}

[System.Serializable]
public struct ShipRequest
{
    public List<ShipSubsystemType> RequiredSubsystems;
    public float? RequiredPower;

    public ShipRequest(List<ShipSubsystemType> _requiredSubsystems, float? _requiredPower)
    {
        RequiredSubsystems = _requiredSubsystems;
        RequiredPower = _requiredPower;
    }
}
