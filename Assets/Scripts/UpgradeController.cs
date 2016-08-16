using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeController : Singleton<UpgradeController>
{

    private UpgradeController() { }

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

        // Clicker Upgrades
        UpgradeObjectDict.Add(16, new UpgradeObject("C1", 50, flatMultiplier, 1, true));
        Debug.Log(UpgradeObjectDict.ToString());
        // TODO: Make sure to actually add the effects of every object that starts with a base level above 0
    }

    private bool checkValidId(int id)
    {
        Debug.Log("ID " + id);
        Debug.Log(UpgradeObjectDict.ToString());
        if (UpgradeObjectDict.ContainsKey(id))
            return true;

        return false;
    }

    private UpgradeObject getUpgradeWithId(int id)
    {
        return UpgradeObjectDict[id];
    }

    // int id refers to the id of the chosen upgrade
    public void purchaseBuildingUpgrade(int id)
    {
        // Check if id is valid
        if (!checkValidId(id))
        {
            Debug.Log("Invalid ID");
            return;
        }

        UpgradeObject obj = getUpgradeWithId(id);


        // Attempt to purchase the upgrade
        obj.purchase();

    }


}
