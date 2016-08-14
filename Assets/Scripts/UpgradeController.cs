using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeController : Singleton<UpgradeController>
{

    private UpgradeController() { }


    //private GameObject[] upgradeList;




    private StatTracker stats;
    private Dictionary<int, UpgradeObject> UpgradeObjectDict;
    private const float flatMultiplier = 1.07f;

    void Start()
    {
        Debug.Log("UpgradeController Start");
        stats = StatTracker.Instance;
        UpgradeObjectDict = new Dictionary<int, UpgradeObject>();

        // Add each possible upgrade to the upgradelist'
        UpgradeObjectDict.Add(1, new UpgradeObject("1", 25, flatMultiplier, 1, false));
        UpgradeObjectDict.Add(2, new UpgradeObject("2", 150, flatMultiplier, 6, false));
        UpgradeObjectDict.Add(3, new UpgradeObject("3", 525, flatMultiplier, 17, false));
        UpgradeObjectDict.Add(4, new UpgradeObject("4", 2400, flatMultiplier, 55, false));
        UpgradeObjectDict.Add(5, new UpgradeObject("5", 11500, flatMultiplier, 195, false));
        UpgradeObjectDict.Add(6, new UpgradeObject("6", 56500, flatMultiplier, 745, false));
        UpgradeObjectDict.Add(7, new UpgradeObject("7", 255000, flatMultiplier, 2180, false));
        UpgradeObjectDict.Add(8, new UpgradeObject("8", 1400000, flatMultiplier, 9425, false));
        UpgradeObjectDict.Add(9, new UpgradeObject("9", 8500000, flatMultiplier, 37200, false));
        UpgradeObjectDict.Add(10, new UpgradeObject("10", 53000000, flatMultiplier, 156400, false));
        UpgradeObjectDict.Add(11, new UpgradeObject("11", 375000000, flatMultiplier, 744200, false));
        UpgradeObjectDict.Add(12, new UpgradeObject("12", 2950000000, flatMultiplier, 3402000, false));
        UpgradeObjectDict.Add(13, new UpgradeObject("13", 26500000000, flatMultiplier, 13812800, false));
        UpgradeObjectDict.Add(14, new UpgradeObject("14", 250000000000, flatMultiplier, 92000000, false));
        UpgradeObjectDict.Add(15, new UpgradeObject("15", 3200000000000, flatMultiplier, 603400000, false));


    }


    // Formula for calculating how much the next upgrade of a building will cost
    private long costFormula(UpgradeObject obj)
    {
        int numPurchased = obj.getNumUpgrades();
        long baseCost = obj.getBaseCost();
        float costMulitplier = obj.getMultiplier();
        long costBeforeGlobal;
        if (numPurchased == 0)
            costBeforeGlobal = baseCost; 
        else
            costBeforeGlobal = (long)(baseCost * Mathf.Pow(costMulitplier, numPurchased)); 

        Debug.Log("Cost Values - numPurchased: " + numPurchased + " baseCost: " + baseCost + " multiplier: " + costMulitplier + " Global Modifiers N/A" + " costBeforeGlobal: " + costBeforeGlobal);

        return costBeforeGlobal; // TODO: Multiply by global modifiers here
    }

    // True iff you can afford upgrade with given id
    private bool canAfford(UpgradeObject obj)
    {
        if (stats.getMoney() > costFormula(obj))
            return true;

        return false;
    }



    // int id refers to the id of the chosen upgrade
    // Attempt to purchase the choosen upgrade
    // This is the method that should be called by the button
    public void buildingUpgrade(int id)
    {
        UpgradeObject obj;
        // Get the UpgradeObject using id.
        if (!UpgradeObjectDict.TryGetValue(id, out obj)) {
            // If ID is invalid exit
            Debug.Log("Invalid Upgrade ID entered");
            return;
        }


        if (canAfford(obj))
        {
            //Debug.Log("Purchasing ID " + obj.getName());
            updateMoneyStats(obj);
            obj.buyUpgrade();
            
        } else
        {
            Debug.Log("Cant afford upgrade");
            // TODO: Respond to invalid amount here (Possibly do nothing)
        }


    }
 
    // Formula for clicker upgrades
    private long clickerUpgradeFormula(UpgradeObject obj)
    {
        return obj.getModifier(); // Currently return a flat amount TODO: Include global modifiers
    }

    private long buildingUpgradeFormula(UpgradeObject obj)
    {
        return obj.getModifier(); // Currently return a flat amount TODO: Include global modifiers
    }

    private void updateMoneyStats(UpgradeObject obj)
    {
        //Debug.Log("Update Money Stats");
        if (obj.isClikerUpgrade()) {
            //Debug.Log("Clicker Upgrade");
            long upgradeVal = clickerUpgradeFormula(obj);
            stats.incrementMoneyPerClick(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula(obj));
        } else
        {
            //Debug.Log("Building Upgrade");
            long upgradeVal = buildingUpgradeFormula(obj);
            stats.incrementeMoneyRate(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula(obj));
        }
    }



   









}
