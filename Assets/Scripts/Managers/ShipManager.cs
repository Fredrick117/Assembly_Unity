using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject rootModule = null;

    private List<GameObject> shipModules = new List<GameObject>();

    public int currentShipCost = 0;
    public ShipClass? currentShipClass;
    public ShipType? currentShipType;
    public float currentShipMinSpeed;
    public float currentShipMaxSpeed;
    public HashSet<Subsystem> currentShipSubsystems = new HashSet<Subsystem>();

    public GameObject currentShip = null;

    [SerializeField]
    private TMP_Text shipTypeText;

    [SerializeField]
    private TMP_Text shipClassText;

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
        //DraggableManager.onPlace += UpdateShipStats;
    }

    private void OnDisable()
    {
        //DraggableManager.onPlace -= UpdateShipStats;
    }

    public void ClearAllShipModules()
    {
        shipModules.Clear();
        rootModule = null;
        currentShipCost = 0;
        currentShipClass = null;
        currentShipType = null;
        currentShipSubsystems.Clear();

        GameObject[] modules = GameObject.FindGameObjectsWithTag("ShipModule");
        foreach (GameObject module in modules)
        {
            GameObject.Destroy(module);
        }
    }

    public void SetShip(GameObject ship)
    {
        shipTypeText.text = ship.GetComponent<ShipBase>().type.ToString();
        shipClassText.text = ship.GetComponent<ShipBase>().shipClass.ToString();

        if (currentShip != null)
            GameObject.Destroy(currentShip);

        currentShip = ship;
    }
}
