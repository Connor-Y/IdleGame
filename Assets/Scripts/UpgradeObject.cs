using UnityEngine;
using System.Collections;

public class UpgradeObject {


    // String Name
    private string objectName;
    // # of upgrades purchased
    private int numOfUpgradesPurchased;
    // upgrade cost modifier
    private int baseUpgradeCost;
    // upgrade cost modifier
    private float baseUpgradeMultiplier;
    // upgrade value modifer at id
    private int baseUpgradeModifier;
    // Is clicker upgrade
    private bool isClicker;
    // Default # of upgrades
    private int defaultLevel;

    public UpgradeObject (string objectName, int baseCost, float baseMultiplier, int baseModifier, bool isClicker, int baseLevel = 0)
    {
        this.objectName = objectName;
        numOfUpgradesPurchased = baseLevel;
        defaultLevel = baseLevel;
        baseUpgradeCost = baseCost;
        baseUpgradeMultiplier = baseMultiplier;
        baseUpgradeModifier = baseModifier;
        this.isClicker = isClicker;
    }
	
    public string getName()
    {
        return objectName;
    }

    public int getBaseCost()
    {
        return baseUpgradeCost;
    }

    public int getNumUpgrades()
    {
        return numOfUpgradesPurchased;
    }

    public float getMultiplier()
    {
        return baseUpgradeMultiplier;
    }

    public int getModifier()
    {
        return baseUpgradeModifier;
    }

    public bool isClikerUpgrade()
    {
        return isClicker;
    }

    public void buyUpgrade()
    {
        numOfUpgradesPurchased += 1;
    }

    public void reset()
    {
        numOfUpgradesPurchased = defaultLevel;
    }

}

