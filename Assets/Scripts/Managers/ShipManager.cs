using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject rootModule = null;

    private List<GameObject> shipModules = new List<GameObject>();

    public int currentDesignCost = 0;
    public ShipClass? currentDesignClass;
    public ShipType? currentDesignType;
    public float currentDesignMinSpeed;
    public float currentDesignMaxSpeed;
    public HashSet<Subsystem> currentDesignSubsystems = new HashSet<Subsystem>();

    public static ShipManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        DraggableManager.onPlace += UpdateShipStats;
    }

    private void OnDisable()
    {
        DraggableManager.onPlace -= UpdateShipStats;
    }

    public void ClearAllShipModules()
    {
        shipModules.Clear();
        rootModule = null;
        currentDesignCost = 0;
        currentDesignClass = null;
        currentDesignType = null;
        currentDesignSubsystems.Clear();

        GameObject[] modules = GameObject.FindGameObjectsWithTag("ShipModule");
        foreach (GameObject module in modules)
        {
            GameObject.Destroy(module);
        }
    }

    private void UpdateShipStats()
    {
        // Get all ship parts

        // 
    }
}
