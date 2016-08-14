using UnityEngine;
using System.Collections;

public class UpgradeObject {


    // String Name
    private string objectName;
    // # of upgrades purchased
    private int numOfUpgradesPurchased;
    // upgrade cost modifier
    private long baseUpgradeCost;
    // upgrade cost modifier
    private float baseUpgradeMultiplier;
    // upgrade value modifer at id
    private long baseUpgradeModifier;
    // Is clicker upgrade
    private bool isClicker;
    // Default # of upgrades
    private int defaultLevel;

    public UpgradeObject (string objectName, long baseCost, float baseMultiplier, long baseModifier, bool isClicker, int baseLevel = 0)
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

    public long getBaseCost()
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

    public long getModifier()
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

