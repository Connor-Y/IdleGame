using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeObject {


    // String Name
    private string objectName;
    // # of upgrades purchased
    private int numOfUpgradesPurchased;
    // upgrade cost modifier
    private long baseUpgradeCost;
    // upgrade cost modifier
    private float baseUpgradeCostMultiplier;
    // upgrade value modifer at id
    private long baseUpgradeModifier;
    // Upgrade value of multiplier at id
    private float baseMultiplierUpgrade;
    // Is clicker upgrade
    private bool isClicker;
    // Default # of upgrades
    private int defaultLevel;
    // instance flat = 0, instance multiplier = 1, both instance flat and multiplier = 2, instance cost multipler = 3
    // permanent rate = 4, permanent cost = 5, permanent flat click = 6
    private int upgradeType;
    // determine whether the upgrade is visible
    private bool isShowing;


protected StatTracker stats;

    public UpgradeObject (string objectName, long baseCost, float baseCostMultiplier, long baseModifier, bool isClicker, int upgradeType = 0, bool isShowing = true, float baseMultiplier = 1f, int baseLevel = 0)
    {
        this.objectName = objectName;
        numOfUpgradesPurchased = baseLevel;
        defaultLevel = baseLevel;
        baseUpgradeCost = baseCost;
        baseUpgradeCostMultiplier = baseCostMultiplier;
        baseUpgradeModifier = baseModifier;
        this.isClicker = isClicker;
        this.upgradeType = upgradeType;
        this.baseMultiplierUpgrade = baseMultiplier;
        this.isShowing = isShowing;
       // if (!isShowing)
         //   toggleVisible();

        stats = StatTracker.Instance;
    }


    // Formula for calculating how much the next upgrade of a building will cost
    private long costFormula()
    {

        long costBeforeGlobal;
        if (numOfUpgradesPurchased == 0)
            costBeforeGlobal = baseUpgradeCost;
        else
            costBeforeGlobal = (long)(baseUpgradeCost * Mathf.Pow(baseUpgradeCostMultiplier, numOfUpgradesPurchased));


        long finalCost = (long)(costBeforeGlobal * stats.getGlobalUpgradeCostMultiplier() * stats.getPermanentUpgradeCostMultiplier());// TODO: Multiply by global modifiers here

       // Debug.Log("Cost Values - numPurchased: " + numOfUpgradesPurchased + " baseCost: " + baseUpgradeCost + " multiplier: " + baseUpgradeCostMultiplier + " Global Modifier " 
       //     + stats.getGlobalUpgradeCostMultiplier() + " Permanement Modifer " + stats.getPermanentUpgradeCostMultiplier() + "\n" + 
       //     " costBeforeGlobal: " + costBeforeGlobal + " finalCost " + finalCost);

        return finalCost;
    }

    // True iff you can afford upgrade with given id
    private bool canAfford()
    {
        if (stats.getMoney() > costFormula())
            return true;

        Debug.Log("Cant afford upgrade");
        return false;
    }


    // Attempt to purchase the choosen upgrade
    // This is the method that should be called by the button
    // returns true if purchase was successful
    protected void purchaseFlat()
    {
        //Debug.Log("Update Money Stats");
        if (isClicker)
        {
            //Debug.Log("Clicker Upgrade");
            stats.incrementMoneyPerClick(baseUpgradeModifier);
        }
        else
        {
            //Debug.Log("Building Upgrade");
            stats.incrementeMoneyRate(baseUpgradeModifier);
        }
    }

    private void purchaseMultiplier()
    {
        if (isClicker)
        {
            stats.incrementGlobalClickMultiplier(baseMultiplierUpgrade);
        }
        else
        {
            stats.incrementGlobalRateMultiplier(baseMultiplierUpgrade);
        }
    }

    private void purchaseBoth()
    {
        purchaseFlat();
        purchaseMultiplier();
    }

    private void costReductionPurchase()
    {
        stats.decrementGlobalCostMultiplier(baseMultiplierUpgrade);
    }


    private void purchasePermanentRateMultiplier()
    {
        if (isClicker)
            stats.incrementPermanentClickMultiplier(baseMultiplierUpgrade);
        else
            stats.incrementPermanentRateMultiplier(baseMultiplierUpgrade);
    }

    private void purchasePermanentUpgradeCostMultiplier()
    {
        stats.decrementPermanentlCostMultiplier(baseMultiplierUpgrade);
    }

    private void purchasePermanentClickIncrement()
    {
        stats.incrementPermanentClickIncrement(baseUpgradeModifier);
    }


    // This method exists soley to be over written in child methods 
    // returns true if purchase was successful
    private void handlePurchase()
    {

        switch (upgradeType)
        {
            case 0:
                purchaseFlat();
                break;
            case 1:
                purchaseMultiplier();
                break;
            case 2:
                purchaseBoth();
                break;
            case 3:
                costReductionPurchase();
                break;
            case 4:
                purchasePermanentRateMultiplier();
                break;
            case 5:
                purchasePermanentUpgradeCostMultiplier();
                break;
            case 6:
                purchasePermanentClickIncrement();
                break;
        }
        // Pay for the upgrade
        stats.decrementMoney(costFormula());
        // Increment the upgrades level
        numOfUpgradesPurchased += 1;

    }

    public bool purchase()
    {
        if (canAfford())
        {
            handlePurchase();
            return true;
        }
        return false;

    }

    // This doesn't work. Try looking up .enabled
/*
    public void toggleVisible()
    {
        
        if (isShowing)
        {
            upgradeObject.SetActive(true);
            return;
        }

        upgradeObject.SetActive(false);
        isShowing = !isShowing;
    }
    */

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
        return baseUpgradeCostMultiplier;
    }

    public long getModifier()
    {
        return baseUpgradeModifier;
    }

    public bool isClikerUpgrade()
    {
        return isClicker;
    }

    public void reset()
    {
        numOfUpgradesPurchased = defaultLevel;
    }

}

