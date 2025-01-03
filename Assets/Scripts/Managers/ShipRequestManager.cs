using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipRequestManager : MonoBehaviour
{
    public RequestData currentShipRequest;

    delegate void CreateNewRequest(RequestData Request);
    CreateNewRequest createNewShipRequest;

    public GameObject textPrefab;

    public Transform requestPanel;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSubmission()
    {
<<<<<<< HEAD
        print("Checking if ship is valid...");
        print("Ship is valid: " + IsValidShip());
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
=======
        CheckAgainstRequest();
        SetNewRequest();
>>>>>>> b61a62f7226ad64a4d4ea21ed388cdc522f35fb8
    }

    /// <summary>
    /// Check to see if the current design satisfies the customer's request
    /// </summary>
    public void CheckAgainstRequest()
    {
        Debug.LogError("Checking against request not yet implemented!");
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
                data.budget = Random.Range(1, 6) * 10000;
                data.minSpeed = 5;
                data.maxSpeed = 10;
                break;
            case ShipClass.Destroyer:
                data.budget = (int)Random.Range(4.0f, 8.5f) * 10000;
                data.minSpeed = 4;
                data.maxSpeed = 8;
                break;
            case ShipClass.Carrier:
                data.budget = Random.Range(10, 20) * 10000;
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

        currentShipRequest = data;

        SetRequestText();
    }

    /// <summary>
    /// Populates the request UI panel with text based on the current request
    /// </summary>
    private void SetRequestText()
    {
        GameObject shipTypeText = GameObject.Instantiate(textPrefab, requestPanel);
        shipTypeText.GetComponent<TMP_Text>().text = "Ship Type: " + currentShipRequest.shipType.ToString();

        GameObject shipClassText = GameObject.Instantiate(textPrefab, requestPanel);
        shipClassText.GetComponent<TMP_Text>().text = "Ship Class: " + currentShipRequest.shipClass.ToString();

        GameObject minSpeedText = GameObject.Instantiate(textPrefab, requestPanel);
        minSpeedText.GetComponent<TMP_Text>().text = "Minimum Speed: " + currentShipRequest.minSpeed.ToString();

        GameObject maxSpeedText = GameObject.Instantiate(textPrefab, requestPanel);
        maxSpeedText.GetComponent<TMP_Text>().text = "Maximum Speed: " + currentShipRequest.maxSpeed.ToString();

        foreach (Subsystem subsystem in currentShipRequest.requiredSubsystems)
        {
            GameObject subsystemText = GameObject.Instantiate(textPrefab, requestPanel);
            subsystemText.GetComponent<TMP_Text>().text = "\t" + subsystem.ToString();
        }

        GameObject budgetText = GameObject.Instantiate(textPrefab, requestPanel);
        budgetText.GetComponent<TMP_Text>().text = "Budget: " + Utilities.IntToFormattedString(currentShipRequest.budget);
    }
}
