using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCount : MonoBehaviour
{
    public int totalMoney = 0;
    private TMP_Text Text;

    private void Awake()
    {
        Text = GetComponent<TMP_Text>();
        Text.text = "Credits: " + totalMoney;
    }

    public void IncrementBalance()
    {
        totalMoney += 50;
        Text.text = "Credits: " + totalMoney;
    }

    public void DecrementBalance()
    {
        totalMoney -= 25;
        Text.text = "Credits: " + totalMoney;
    }
}
