using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalMoneySystem : MonoBehaviour
{
    public int balance;
    [SerializeField] public TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        moneyText.text = "MONEY: 00";
    }

    /// <summary>
    /// Use this to if player has enough money for a specific thing. For example when a item is 100$, you would do HasMoney(100)
    /// </summary>
    /// <param name="amount">Amount you want to check if the player has.</param>
    /// <returns>True or false</returns>
    public bool HasMoney(int amount)
    {
        return balance >= amount;
    }

    /// <summary>
    /// Used to give the player money.
    /// </summary>
    /// <param name="amount">Amount to give to the player.</param>
    public void GiveMoney(int amount)
    {
        balance += amount;
        UpdateText();
    }

    /// <summary>
    /// Used to take money from the player. Keep in mind, when the player has 50 for example, and you take 60, the number won't go negative, so the player's money would be 0, instead of -10.
    /// </summary>
    /// <param name="amount">Amount of money to take.</param>
    public void TakeMoney(int amount)
    {
        balance -= amount;
        if (balance < 0) balance = 0;
        UpdateText();
    }

    /// <summary>
    /// Check what amount of money the player has
    /// </summary>
    /// <returns>Balance of player</returns>
    public int GetMoney()
    {
        return balance;
    }

    public void UpdateText()
    {
        if (balance < 9)
        {
            moneyText.text = "MONEY: 0" + balance.ToString();
        }
        else
        {
            moneyText.text = "MONEY: " + balance.ToString();
        }
    }
}
