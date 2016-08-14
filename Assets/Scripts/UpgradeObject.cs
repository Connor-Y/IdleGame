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

    protected StatTracker stats;

    public UpgradeObject (string objectName, long baseCost, float baseMultiplier, long baseModifier, bool isClicker, int baseLevel = 0)
    {
        this.objectName = objectName;
        numOfUpgradesPurchased = baseLevel;
        defaultLevel = baseLevel;
        baseUpgradeCost = baseCost;
        baseUpgradeMultiplier = baseMultiplier;
        baseUpgradeModifier = baseModifier;
        this.isClicker = isClicker;
        stats = StatTracker.Instance;
    }


    // Formula for calculating how much the next upgrade of a building will cost
    private long costFormula()
    {

        long costBeforeGlobal;
        if (numOfUpgradesPurchased == 0)
            costBeforeGlobal = baseUpgradeCost;
        else
            costBeforeGlobal = (long)(baseUpgradeCost * Mathf.Pow(baseUpgradeMultiplier, numOfUpgradesPurchased));


        long finalCost = (long)(costBeforeGlobal * stats.getGlobalUpgradeCostMultiplier() * stats.getPermanentUpgradeCostMultiplier());// TODO: Multiply by global modifiers here

        Debug.Log("Cost Values - numPurchased: " + numOfUpgradesPurchased + " baseCost: " + baseUpgradeCost + " multiplier: " + baseUpgradeMultiplier + " Global Modifier " 
            + stats.getGlobalUpgradeCostMultiplier() + " Permanement Modifer " + stats.getPermanentUpgradeCostMultiplier() + "\n" + 
            " costBeforeGlobal: " + costBeforeGlobal + " finalCost " + finalCost);

        return finalCost;
    }

    // True iff you can afford upgrade with given id
    private bool canAfford()
    {
        if (stats.getMoney() > costFormula())
            return true;

        return false;
    }

    // Formula for clicker upgrades
    private long clickerUpgradeFormula()
    {
        return (long) (baseUpgradeModifier * (stats.getGlobalClickMultiplier() * stats.getPermanentClickMultiplier())) + stats.getPermanentClickIncrement(); // TODO: Include additional modifiers
    }

    private long buildingUpgradeFormula()
    {
        return (long)(baseUpgradeModifier * stats.getGlobalRateMultiplier() * stats.getPermanentRateMultiplier()); // TODO: Include additional modifiers
    }

    private void updateMoneyStats()
    {
        //Debug.Log("Update Money Stats");
        if (isClicker)
        {
            //Debug.Log("Clicker Upgrade");
            long upgradeVal = clickerUpgradeFormula();
            stats.incrementMoneyPerClick(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula());
        }
        else
        {
            //Debug.Log("Building Upgrade");
            long upgradeVal = buildingUpgradeFormula();
            stats.incrementeMoneyRate(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula());
        }
    }

    // Attempt to purchase the choosen upgrade
    // This is the method that should be called by the button
    // returns true if purchase was successful
    protected bool basicPurchase()
    {
        if (canAfford())
        {
            //Debug.Log("Purchasing ID " + obj.getName());
            updateMoneyStats();
            buyUpgrade();
            return true;
        }
        else
        {
            Debug.Log("Cant afford upgrade");
            return false;
            // TODO: Respond to invalid amount here (Possibly do nothing)
        }


    }

    // This method exists soley to be over written in child methods 
    // returns true if purchase was successful
    public void purchase()
    {
        basicPurchase();
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

