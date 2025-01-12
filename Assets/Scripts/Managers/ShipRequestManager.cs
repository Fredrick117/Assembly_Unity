using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShipRequestManager : MonoBehaviour
{
    public RequestData activeShipRequest;

    delegate void CreateNewRequest(RequestData Request);
    CreateNewRequest createNewShipRequest;

    public TMP_Text requestText;

    //public TextAsset requestData;

    private void OnEnable()
    {
        EventManager.onSubmit += OnSubmission;
    }

    private void OnDisable()
    {
        EventManager.onSubmit -= OnSubmission;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNewRequest();
        SetRequestText();
    }

    private void OnSubmission()
    {
        print("Checking if ship is valid...");
        print("Ship is valid: " + IsValidShip());

        GameObject[] modules = GameObject.FindGameObjectsWithTag("ShipModule");
        SubmitDesign();

        ShipManager.Instance.ClearAllShipModules();

        SubmitDesign();
    }

    /// <summary>
    /// Checks if the current design is a valid ship design.
    /// A valid ship design:
    ///     - Does not have any exposed segments
    ///     - Has an engine/thrusters
    ///     - Has at least two modules
    /// </summary>
    /// <returns>Whether or not the design is valid</returns>
    private bool IsValidShip()
    {
        GameObject root = ShipManager.Instance.rootModule;

        if (root == null)
        {
            return false;
        }

        foreach (Connector connector in root.GetComponent<ShipModule>().connectors)
        {
            if (!connector.otherConnector)
            {
                return false;
            }
        }

        return true;
    }

    public void SubmitDesign()
    {
        float rewardModifier = 1.0f;
        if (activeShipRequest.budget != null)
        {
            if (ShipManager.Instance.currentDesignCost < activeShipRequest.budget)
            {
                rewardModifier += 0.2f;
            }
            else
            {
                rewardModifier -= 0.3f;
            }
        }


        if (rewardModifier <= 0.0f)
        {
            rewardModifier = 0.0f;
        }

        GameManager.Instance.UpdateCredits(Mathf.RoundToInt(activeShipRequest.reward * rewardModifier));
    }

    public void SetRequestText()
    {
        requestText.text = "<b>Ship Type:</b> " + activeShipRequest.shipType.ToString() + "\n" + 
                           "<b>Ship Class:</b> " + activeShipRequest.shipClass.ToString() + "\n" +
                           "<b>Mininum Speed:</b> " + activeShipRequest.minSpeed.ToString() + "\n" +
                           "<b>Maximum Speed:</b> " + activeShipRequest.maxSpeed.ToString() + "\n\n";

        requestText.text += "<b>Required Subsystems:</b>\n";

        foreach (Subsystem subsystem in activeShipRequest.requiredSubsystems)
        {
            requestText.text += "\t" + subsystem.ToString() + "\n";
        }

        requestText.text += "\n<b>Budget:</b> " + activeShipRequest.budget.ToString();
    }

    /// <summary>
    /// Creates a random request for a ship and sets it to the current ship request.
    /// </summary>
    public void SetNewRequest()
    {
        // TODO: get data from JSON file so that random distribution isn't equal
        
        RequestData data = new RequestData();
        data.shipType = Utilities.GetRandomEnumValue<ShipType>();
        data.shipClass = Utilities.GetRandomEnumValue<ShipClass>();

        switch (data.shipClass)
        {
            case ShipClass.Corvette:
                data.budget = Random.Range(10000, 50000);
                data.minSpeed = 5;
                data.maxSpeed = 10;
                break;
            case ShipClass.Destroyer:
                data.budget = Random.Range(40000, 75000);
                data.minSpeed = 4;
                data.maxSpeed = 8;
                break;
            case ShipClass.Carrier:
                data.budget = Random.Range(100000, 200000);
                data.minSpeed = 1;
                data.maxSpeed = 3;
                break;
            default:
                data.budget = 0;
                data.minSpeed = 0;
                data.maxSpeed = 0;
                break;
        }

        data.requiredSubsystems = new HashSet<Subsystem>();
        
        for (int i = 0; i < Random.Range(1, System.Enum.GetNames(typeof(Subsystem)).Length); i++)
        {
            data.requiredSubsystems.Add(Utilities.GetRandomEnumValue<Subsystem>());
        }

        activeShipRequest = data;
    }
}
