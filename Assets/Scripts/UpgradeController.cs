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
        UpgradeObjectDict.Add(2, new UpgradeObject("2", 150, flatMultiplier, 5, false));
        UpgradeObjectDict.Add(3, new UpgradeObject("3", 525, flatMultiplier, 15, false));


    }


    // Formula for calculating how much the next upgrade of a building will cost
    private int costFormula(UpgradeObject obj)
    {
        int numPurchased = obj.getNumUpgrades();
        int baseCost = obj.getBaseCost();
        float costMulitplier = obj.getMultiplier();
        int costBeforeGlobal;
        if (numPurchased == 0)
            costBeforeGlobal = baseCost; 
        else
            costBeforeGlobal = (int)(baseCost * Mathf.Pow(costMulitplier, numPurchased)); 

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
    private int clickerUpgradeFormula(UpgradeObject obj)
    {
        return obj.getModifier(); // Currently return a flat amount TODO: Include global modifiers
    }

    private int buildingUpgradeFormula(UpgradeObject obj)
    {
        return obj.getModifier(); // Currently return a flat amount TODO: Include global modifiers
    }

    private void updateMoneyStats(UpgradeObject obj)
    {
        //Debug.Log("Update Money Stats");
        if (obj.isClikerUpgrade()) {
            //Debug.Log("Clicker Upgrade");
            int upgradeVal = clickerUpgradeFormula(obj);
            stats.incrementMoneyPerClick(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula(obj));
        } else
        {
            //Debug.Log("Building Upgrade");
            int upgradeVal = buildingUpgradeFormula(obj);
            stats.incrementeMoneyRate(upgradeVal);
            // Pay the cost of the building
            stats.decrementMoney(costFormula(obj));
        }
    }



   









}
