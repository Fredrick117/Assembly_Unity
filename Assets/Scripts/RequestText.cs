using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestText : MonoBehaviour
{
    private TMP_Text Text;
    private void Awake()
    {


    }

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TMP_Text>();
        SetText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText()
    {
        Text.text = "<color=#FFC500>" + GameManager.Instance.CurrentShipRequest.shipClass.ToString() + "</color>-class";
    }
}
