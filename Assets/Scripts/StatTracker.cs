using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatTracker : Singleton<StatTracker>
{

    public Text moneyText;

    private long timeStamp;

    private long money;
    private long moneyPerClick;
    private long moneyRate;

    // Global Modifers
    private float globalRateMultiplier;
    private float globalClickMultiplier;
    private float globalUpgradeCostMultiplier;

    // Permanent Modifers (Remain through prestige)
    private float permanentRateMultiplier;
    private float permanentClickMultiplier;
    private float permanentUpgradeCostMultiplier;
    private long permanentClickIncrement;




    void Awake()
    {
        Debug.Log("Stats Awake");
        timeStamp = getUnixTime(); // TODO: Figure out how to store unix timestamp on app close or shutdown 
        money = 10000; // TODO: Set to decided starting value
        moneyPerClick = 1;
        globalRateMultiplier = 0f;
        globalClickMultiplier = 0f; 
        globalUpgradeCostMultiplier = 1f;

        permanentRateMultiplier = 0f;
        permanentClickMultiplier = 0f;
        permanentUpgradeCostMultiplier = 1f;
        permanentClickIncrement = 0;

    }
 
    // Private to prevent being called outside of singleton instance.
    private StatTracker() { }

    public long getMoney()
    { 
        return money;
    }

    public long getMoneyPerClick()
    {
        return moneyPerClick;
    }

    public long getMoneyRate()
    {
        return moneyRate;
    }

    public float getGlobalRateMultiplier()
    {
        return globalRateMultiplier;
    }

    public float getGlobalClickMultiplier()
    {
        return globalClickMultiplier;
    }

    public float getGlobalUpgradeCostMultiplier()
    {
        return globalUpgradeCostMultiplier;
    }


    public float getPermanentRateMultiplier()
    {
        return permanentRateMultiplier;
    }

    public float getPermanentClickMultiplier()
    {
        return permanentClickMultiplier;
    }

    public float getPermanentUpgradeCostMultiplier()
    {
        return permanentUpgradeCostMultiplier;
    }

    public long getPermanentClickIncrement()
    {
        return permanentClickIncrement;
    }

    public void incrementGlobalRateMultiplier(float x)
    {
        globalRateMultiplier += x;
    }

    public void incrementGlobalClickMultiplier(float x)
    {
        globalClickMultiplier += x;
    }

    public void decrementGlobalCostMultiplier(float x)
    {
        globalUpgradeCostMultiplier -= x;
    }

    public void incrementPermanentRateMultiplier(float x)
    {
        permanentRateMultiplier += x;
    }

    public void incrementPermanentClickMultiplier(float x)
    {
        permanentClickMultiplier += x;
    }

    public void decrementPermanentlCostMultiplier(float x)
    {
        permanentUpgradeCostMultiplier -= x;
    }

    public void incrementPermanentClickIncrement(long x)
    {
        permanentClickIncrement += x;
    }

    public void incrementMoney(long x)
    {
        money += x;
    }

    public void decrementMoney(long x)
    {
        money -= x;
    }

    public void incrementMoneyPerClick(long x)
    {
        moneyPerClick += x;
    }
    public void incrementeMoneyRate(long x)
    {
        moneyRate += x;
    }

    public void resetMoneyValues()
    {
        money = 0;
        moneyRate = 0;
        moneyPerClick = 1;
        globalClickMultiplier = 1f; 
        globalClickMultiplier = 1f; 
        globalUpgradeCostMultiplier = 1f; 
    }

    public void hardReset()
    {
        resetMoneyValues();
        permanentClickIncrement = 0;
        permanentClickMultiplier = 1f;
        permanentRateMultiplier = 1f;
        permanentUpgradeCostMultiplier = 1f;
    }

    public void updateMoneyText()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public long getUnixTime()
    {
        return (long)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a second has passed, if so increase money by moneyRate
        // TODO: Change this to run off of a servers time. Need a way to handle offline mode.
        if (timeStamp < getUnixTime())
        {
            money += (long) ((moneyRate * (int)(getUnixTime() - timeStamp)) * (1 + globalRateMultiplier + permanentRateMultiplier));
            timeStamp = getUnixTime();
        }
        updateMoneyText();

    }

}
