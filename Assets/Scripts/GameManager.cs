using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ShipRequest CurrentShipRequest;

    delegate void TestFunc(ShipRequest Request);
    TestFunc CreateNewShipRequest;

    [SerializeField]
    private int numSubsystemTypes = 4;

    private GameObject AssemblyAreaObject;
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

        // List<ShipSubsystemType> list = new List<ShipSubsystemType>();

        // // Add a few random required subsystems
        // for (int i = 0; i < numSubsystemTypes; i++)
        // {
        //     list.Add((ShipSubsystemType)Random.Range(0, (int)System.Enum.GetValues(typeof(ShipSubsystemType)).Cast<ShipSubsystemType>().Max()));
        // }

        // CurrentShipRequest = new ShipRequest(list, Random.Range(10.0f, 30.0f));

        CurrentShipRequest = CreateRandomRequest(numSubsystemTypes);

        AssemblyAreaObject = GameObject.FindGameObjectWithTag("AssemblyArea");
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSubmission += CheckAgainstRequest;
    }

    // Check to see if the ship in the assembly area matches the requirements provided
    void CheckAgainstRequest()
    {
        List<ShipSubsystemType> SatisfiedRequests = new List<ShipSubsystemType>();
        List<GameObject> ShipParts = AssemblyAreaObject.GetComponent<AssemblyArea>().ObjectsInArea;

        if (ShipParts.Count > 0)
        {
            foreach (var Object in ShipParts)
            {
                foreach (Transform t in Object.gameObject.transform)
                {
                    if (t.gameObject.tag == "Room")
                    {
                        Room room = t.gameObject.GetComponent<Room>();
                        if (room.InstalledSubsystems != null)
                        {
                            foreach (var subsystem in room.InstalledSubsystems)
                            {
                                if (subsystem.tag == "Reactor" && !SatisfiedRequests.Contains(ShipSubsystemType.REACTOR))
                                {
                                    SatisfiedRequests.Add(ShipSubsystemType.REACTOR);
                                }
                                else if (subsystem.tag == "ShieldGen" && !SatisfiedRequests.Contains(ShipSubsystemType.SHIELDS))
                                {
                                    SatisfiedRequests.Add(ShipSubsystemType.SHIELDS);
                                }
                                else if (subsystem.tag == "LifeSup" && !SatisfiedRequests.Contains(ShipSubsystemType.LIFE_SUPPORT))
                                {
                                    SatisfiedRequests.Add(ShipSubsystemType.LIFE_SUPPORT);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            print("There's nothing here!");
        }

        if (SatisfiedRequests.All(CurrentShipRequest.RequiredSubsystems.Contains) && CurrentShipRequest.RequiredSubsystems.Count == SatisfiedRequests.Count)
        {
            print("nice");
            CleanUpObjects();
            MoneyCounter.GetComponent<MoneyCount>().IncrementBalance();
            CurrentShipRequest = CreateRandomRequest(numSubsystemTypes);
            RequestUI.GetComponent<RequestText>().SetText();
        }
        else
        {
            print("no");
            MoneyCounter.GetComponent<MoneyCount>().DecrementBalance();
        }
    }

    ShipRequest CreateRandomRequest(int numTypes)
    {
        List<ShipSubsystemType> list = new List<ShipSubsystemType>();

        // Add a few random required subsystems
        for (int i = 0; i < numTypes; i++)
        {
            ShipSubsystemType subsystem = GetRandomSubsystem();
            if (!list.Contains(subsystem))
            {
                list.Add(subsystem);
            }
        }

        ShipRequest request = new ShipRequest(list, Random.Range(10.0f, 30.0f));

        return request;
    }

    ShipSubsystemType GetRandomSubsystem()
    {
        return (ShipSubsystemType)Random.Range(0, (int)System.Enum.GetValues(typeof(ShipSubsystemType)).Cast<ShipSubsystemType>().Max());
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
