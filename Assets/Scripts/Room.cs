using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsOccupied = false;
    public List<GameObject> InstalledSubsystems;

    private void Awake()
    {
        InstalledSubsystems = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LifeSup" || other.tag == "Reactor" || other.tag == "ShieldGen")
        {
            IsOccupied = true;
            ReactorSubsystem Data = (ReactorSubsystem)other.gameObject.GetComponent<DraggableComponent>().SubsystemData;
            InstalledSubsystems.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LifeSup" || other.tag == "Reactor" || other.tag == "ShieldGen")
        {
            IsOccupied = false;
            InstalledSubsystems.Remove(other.gameObject);
        }
    }
}
