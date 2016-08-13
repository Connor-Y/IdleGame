using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatTracker : Singleton<StatTracker>
{
    public Text moneyText;

    private static StatTracker instance;

    private int money;
    private int moneyRate;
    private int moneyPerClick;

    private GameObject[] upgradeList;
    private int numOfUpgrades;
    private int[] upgradesPurchased;
    private int[] upgradeModifier;
    private int[] upgradeCost;
    private long timeStamp;

    void Awake()
    {
        money = 0;
        moneyRate = 0;
        moneyPerClick = 1;
        //upgradeList = GameObject.FindGameObjectsWithTag("Upgrade");
        //numOfUpgrades = upgradeList.Length;
        numOfUpgrades = 3;
        upgradesPurchased = new int[numOfUpgrades];
        upgradeModifier = new int[numOfUpgrades];
        upgradeCost = new int[numOfUpgrades];
        timeStamp = getUnixTime(); // TODO: Figure out how to store unix timestamp on app close or shutdown 
                                   // TODO: Figure out how to get an online timestamp as opposed to using the system timestamp (system redundancy?)
        for (int i = 0; i < numOfUpgrades; i++)
        {
            upgradesPurchased[i] = 0;
            upgradeCost[i] = 10; // TODO: Remove and set proper values
            upgradeModifier[i] = 2; // TODO: Remove and set proper values
        }
    }
 
    // Private to prevent being called outside of singleton instance.
    private StatTracker()
    {
        
    }

    public int getMoney()
    {
        return money;
    }

    public void buttonPressed()
    {
        money += moneyPerClick;
    }

    public void incrementMoney(int x)
    {
        money += x;
    }

    public int getMoneyRate()
    {
        return moneyRate;
    }

    public void incrementeMoneyRate(int x)
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
        if (timeStamp < getUnixTime())
        {
            money += moneyRate * (int)(getUnixTime() - timeStamp);
            timeStamp = getUnixTime();
        }
        updateMoneyText();

    }
}
