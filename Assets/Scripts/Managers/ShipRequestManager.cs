using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShipRequestManager : MonoBehaviour
{
    public ShipRequest currentShipRequest;

    delegate void TestFunc(ShipRequest Request);
    TestFunc createNewShipRequest;

    [SerializeField]
    private int numSubsystemTypes = 4;

    public GameObject requestUI;

    private TMP_Text requestText;

    private GameObject textPrefab;

    private void Awake()
    {
        ShipType shipType = Utilities.GetRandomEnumValue<ShipType>();
        float maxMoney = Random.Range(7500f, 10000f);
        int minSpeed = Random.Range(5, 10);
        
        print("SHIP TYPE: " + shipType.ToString() + " | MAX MONEY: " + maxMoney.ToString() + " | MIN SPEED: " + minSpeed.ToString());
        
        // List<ShipSubsystemType> list = new List<ShipSubsystemType>();

        // // Add a few random required subsystems
        // for (int i = 0; i < numSubsystemTypes; i++)
        // {
        //     list.Add((ShipSubsystemType)Random.Range(0, (int)System.Enum.GetValues(typeof(ShipSubsystemType)).Cast<ShipSubsystemType>().Max()));
        // }

        // currentShipRequest = new ShipRequest(list, Random.Range(10.0f, 30.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnSubmission += CheckAgainstRequest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentRequest()
    {
        currentShipRequest = CreateRandomRequest(numSubsystemTypes);
    }

    // Check to see if the ship in the assembly area matches the requirements provided
    public void CheckAgainstRequest()
    {
        //List<ShipSubsystemType> SatisfiedRequests = new List<ShipSubsystemType>();
        //List<GameObject> ShipParts = assemblyAreaObject.GetComponent<AssemblyArea>().ObjectsInArea;

        //if (ShipParts.Count > 0)
        //{
        //    foreach (var Object in ShipParts)
        //    {
        //        foreach (Transform t in Object.gameObject.transform)
        //        {
        //            if (t.gameObject.tag == "Room")
        //            {
        //                //Room room = t.gameObject.GetComponent<Room>();
        //                //if (room.InstalledSubsystems != null)
        //                //{
        //                //    foreach (var subsystem in room.InstalledSubsystems)
        //                //    {
        //                //        if (subsystem.tag == "Reactor" && !SatisfiedRequests.Contains(ShipSubsystemType.REACTOR))
        //                //        {
        //                //            SatisfiedRequests.Add(ShipSubsystemType.REACTOR);
        //                //        }
        //                //        else if (subsystem.tag == "ShieldGen" && !SatisfiedRequests.Contains(ShipSubsystemType.SHIELDS))
        //                //        {
        //                //            SatisfiedRequests.Add(ShipSubsystemType.SHIELDS);
        //                //        }
        //                //        else if (subsystem.tag == "LifeSup" && !SatisfiedRequests.Contains(ShipSubsystemType.LIFE_SUPPORT))
        //                //        {
        //                //            SatisfiedRequests.Add(ShipSubsystemType.LIFE_SUPPORT);
        //                //        }
        //                //    }
        //                //}
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    print("There's nothing here!");
        //}

        //if (SatisfiedRequests.All(currentShipRequest.RequiredSubsystems.Contains) && currentShipRequest.RequiredSubsystems.Count == SatisfiedRequests.Count)
        //{
        //    print("nice");
        //    CleanUpObjects();
        //    moneyCounter.GetComponent<MoneyCount>().IncrementBalance();
        //    currentShipRequest = CreateRandomRequest(numSubsystemTypes);
        //    requestUI.GetComponent<RequestText>().SetText();
        //}
        //else
        //{
        //    print("no");
        //    moneyCounter.GetComponent<MoneyCount>().DecrementBalance();
        //}
    }

    private ShipRequest CreateRandomRequest(int numTypes)
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

    private ShipSubsystemType GetRandomSubsystem()
    {
        return (ShipSubsystemType)Random.Range(0, (int)System.Enum.GetValues(typeof(ShipSubsystemType)).Cast<ShipSubsystemType>().Max());
    }

    public void SetRequestText()
    {
        requestText.text = "";
        foreach (ShipSubsystemType subsystem in currentShipRequest.RequiredSubsystems)
        {
            requestText.text += (subsystem + "\n");
        }
    }

}
