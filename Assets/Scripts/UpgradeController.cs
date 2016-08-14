using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeController : Singleton<UpgradeController>
{

    private UpgradeController() { }


    //private GameObject[] upgradeList;

    // # of upgrades purchased at given id
    private Dictionary<int, int> numOfUpgradesPurchased;
    // upgrade cost modifier at id
    private Dictionary<int, int> baseUpgradeCost;
    // upgrade cost modifier at id
    private Dictionary<int, float> baseUpgradeMultiplier;
    // upgrade value modifer at id
    private Dictionary<int, int> baseUpgradeModifier;
    // List of id's which are clicker upgrades;
    private List<int> clickerUpgradeList;


    private StatTracker stats;

    private int numOfUpgrades;

    void Start()
    {
        Debug.Log("UpgradeController Start");
        stats = StatTracker.Instance;
        numOfUpgrades = 3;
 
        numOfUpgradesPurchased = new Dictionary<int, int>();
        baseUpgradeCost = new Dictionary<int, int>();
        baseUpgradeMultiplier = new Dictionary<int, float>();
        baseUpgradeModifier = new Dictionary<int, int>();
        clickerUpgradeList = new List<int>();



        for (int i = 0; i < numOfUpgrades; i++)
        {
            numOfUpgradesPurchased.Add(i, 0);
            // TODO: Remove and set proper values
            baseUpgradeCost.Add(i, 10); 
            baseUpgradeMultiplier.Add(i, 1.07f);
            baseUpgradeModifier.Add(i, 2); 
        }
    }


    // Formula for calculating how much the next upgrade of a building will cost
    private int costFormula(int id)
    {
        int numPurchased = numOfUpgradesPurchased[id];
        int baseCost = baseUpgradeCost[id];
        float costMulitplier = baseUpgradeMultiplier[id];
        int finalCost = (int)(baseCost * Mathf.Pow(costMulitplier, numPurchased));

        Debug.Log("Cost Values - numPurchased: " + numPurchased + " baseCost: " + baseCost + " multiplier: " + costMulitplier + " finalCost: " + finalCost);

        return finalCost;
    }

    // True iff you can afford upgrade with given id
    private bool canAfford(int id)
    {
        if (stats.getMoney() > costFormula(id))
            return true;

        return false;
    }



    // int id refers to the id of the chosen upgrade
    // Attempt to purchase the choosen upgrade
    // This is the method that should be called by the button
    public void buildingUpgrade(int id)
    {
        int val;
        float fval;
        bool validId = true;

        // Check to make sure the given ID is valid in all dicts
        if (!numOfUpgradesPurchased.TryGetValue(id, out val))
        {
            validId = false;
        }
        else if (!baseUpgradeCost.TryGetValue(id, out val))
        {
            validId = false;
        }
        if (!baseUpgradeMultiplier.TryGetValue(id, out fval))
        {
            validId = false;
        }
        else if (!baseUpgradeModifier.TryGetValue(id, out val))
        {
            validId = false;
        }
        
        if (!validId)
        {
            Debug.Log("ID " + id + " was not found");
            return;
        }

        if (canAfford(id))
        {
            Debug.Log("Purchasing ID " + id);
            numOfUpgradesPurchased[id] += 1;
            updateMoneyStats(id);
        } else
        {
            Debug.Log("Cant afford upgrade");
            // TODO: Respond to invalid amount here (Possibly do nothing)
        }


    }

    // Decrease money by value counting modifies 
    private int purchaseFormula(int id)
    {
        return (-1 * costFormula(id)); // TODO: Set to include any modifiers when they are added.
    }
    
    // Formula for clicker upgrades
    private int clickerUpgradeFormula(int id)
    {
        return baseUpgradeModifier[id]; // Currently return a flat amount TODO: Include modifiers
    }

    private int buildingUpgradeFormula(int id)
    {
        return baseUpgradeModifier[id]; // Currently return a flat amount TODO: Include modifiers
    }

    private void updateMoneyStats(int id)
    {
        Debug.Log("Update Money Stats");
        if (clickerUpgradeList.Contains(id)) {
            Debug.Log("Clicker Upgrade");
            int upgradeVal = clickerUpgradeFormula(id);
            stats.incrementMoneyPerClick(upgradeVal);
            // Pay the cost of the building
            stats.incrementMoney(purchaseFormula(id));
        } else
        {
            Debug.Log("Building Upgrade");
            int upgradeVal = buildingUpgradeFormula(id);
            stats.incrementeMoneyRate(upgradeVal);
            // Pay the cost of the building
            stats.incrementMoney(purchaseFormula(id));
        }
    }



   









}
