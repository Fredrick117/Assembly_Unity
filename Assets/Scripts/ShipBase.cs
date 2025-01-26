using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    public float hullRating = 0f;
    public float maxSpeed = 0f;
    public float damagePerSecond = 0f;
    public int numSubsystemSlots = 0;
    public int numArmamentSlots = 0;
    public ShipType type;
    public ShipClass shipClass;
}
