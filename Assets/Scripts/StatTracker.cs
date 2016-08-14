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

    void Awake()
    {
        Debug.Log("Stats Awake");
        timeStamp = getUnixTime(); // TODO: Figure out how to store unix timestamp on app close or shutdown 
        money = 100; // TODO: Set to decided starting value
        moneyPerClick = 1;
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
            money += moneyRate * (int)(getUnixTime() - timeStamp);
            timeStamp = getUnixTime();
        }
        updateMoneyText();

    }
}
