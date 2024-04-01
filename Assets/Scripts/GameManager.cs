using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isDragging;
    public GameObject draggedObject;

    public static GameManager Instance { get; private set; }

    public ShipRequest CurrentShipRequest;

    delegate void ShipRequestDelegate(ShipRequest Request);
    ShipRequestDelegate CreateNewShipRequest;

    [SerializeField]
    private int numSubsystemTypes = 4;

    public GameObject RequestUI;

    public GameObject MoneyCounter;

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

        CurrentShipRequest = CreateRandomRequest(numSubsystemTypes);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSubmission += CheckAgainstRequest;
    }

    // Check to see if the ship in the assembly area matches the requirements provided
    void CheckAgainstRequest()
    {

    }

    ShipRequest CreateRandomRequest(int numTypes)
    {
        List<SubsystemType> list = new List<SubsystemType>();

        // Add a few random required subsystems
        for (int i = 0; i < numTypes; i++)
        {
            SubsystemType subsystem = GetRandomSubsystem();
            if (!list.Contains(subsystem))
            {
                list.Add(subsystem);
            }
        }

        ShipRequest request = new ShipRequest();
        request.shipClass = (ShipClass)Random.Range(0, (int)System.Enum.GetValues(typeof(ShipClass)).Cast<ShipClass>().Max());

        return request;
    }

    SubsystemType GetRandomSubsystem()
    {
        return (SubsystemType)Random.Range(0, (int)System.Enum.GetValues(typeof(SubsystemType)).Cast<SubsystemType>().Max());
    }

    void CleanUpObjects()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ModuleBase"))
        {
            GameObject.Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Reactor"))
        {
            GameObject.Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ShieldGen"))
        {
            GameObject.Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LifeSup"))
        {
            GameObject.Destroy(go);
        }
    }
}
