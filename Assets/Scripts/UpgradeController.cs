using UnityEngine;
using System.Collections.Generic;


public class UpgradeController : Singleton<UpgradeController>
{

    private UpgradeController() { }

    private Dictionary<int, UpgradeObject> UpgradeObjectDict;
    private const float flatMultiplier = 1.07f;
    private int numOfUpgrades = 0;

    public GameObject keyboard;
    public GameObject chair;


    void Awake()
    {
        Debug.Log("UpgradeController Start");
        UpgradeObjectDict = new Dictionary<int, UpgradeObject>();

        // public UpgradeObject (string objectName, long baseCost, float baseCostMultiplier, long baseModifier, bool isClicker,
        // int maxLevel = 0, int upgradeType = 0, float baseMultiplier = 1f, int baseLevel = 0)

        // instance flat = 0, instance multiplier = 1, both instance flat and multiplier = 2, instance cost multipler = 3
        // permanent rate = 4, permanent cost = 5, permanent flat click = 6


        
        // Add each possible upgrade to the upgradelist
        UpgradeObjectDict.Add(1, new UpgradeObject("G1", 25, flatMultiplier, 1, false));
        UpgradeObjectDict.Add(2, new UpgradeObject("G2", 150, flatMultiplier, 6, false));
        UpgradeObjectDict.Add(3, new UpgradeObject("G3", 525, flatMultiplier, 17, false));
        UpgradeObjectDict.Add(4, new UpgradeObject("G4", 2400, flatMultiplier, 55, false));
        UpgradeObjectDict.Add(5, new UpgradeObject("G5", 11500, flatMultiplier, 195, false));
        UpgradeObjectDict.Add(6, new UpgradeObject("G6", 56500, flatMultiplier, 745, false));
        UpgradeObjectDict.Add(7, new UpgradeObject("G7", 255000, flatMultiplier, 2180, false));
        UpgradeObjectDict.Add(8, new UpgradeObject("G8", 1400000, flatMultiplier, 9425, false));
        UpgradeObjectDict.Add(9, new UpgradeObject("G9", 8500000, flatMultiplier, 37200, false));
        UpgradeObjectDict.Add(10, new UpgradeObject("G10", 53000000, flatMultiplier, 156400, false));
        UpgradeObjectDict.Add(11, new UpgradeObject("G11", 375000000, flatMultiplier, 744200, false));
        UpgradeObjectDict.Add(12, new UpgradeObject("G12", 2950000000, flatMultiplier, 3402000, false));
        UpgradeObjectDict.Add(13, new UpgradeObject("G13", 26500000000, flatMultiplier, 13812800, false));
        UpgradeObjectDict.Add(14, new UpgradeObject("G14", 250000000000, flatMultiplier, 92000000, false));
        UpgradeObjectDict.Add(15, new UpgradeObject("G15", 3200000000000, flatMultiplier, 603400000, false));

        // Clicker Flat Upgrades
        UpgradeObjectDict.Add(16, new UpgradeObject("C1", 50, flatMultiplier, 0, true));

        // Global Rate Mulitpliers
        UpgradeObjectDict.Add(17, new UpgradeObject("GR1", 10, flatMultiplier, 0, false, 1, 1f, 2));
        // Clicker Rate upgrade
        UpgradeObjectDict.Add(18, new UpgradeObject("GCR1", 50, flatMultiplier, 0, true, 1, 1f, 9));

        // Global Cost Reduction
        UpgradeObjectDict.Add(19, new UpgradeObject("GC1", 10, flatMultiplier, 5, false, 3, 1f, 1));

        // Perm Rate
        UpgradeObjectDict.Add(20, new UpgradeObject("PR1", 10, flatMultiplier, 5, false, 4, 1f, 1));

        // Perm Cost Reduction
        UpgradeObjectDict.Add(21, new UpgradeObject("PC1", 10, flatMultiplier, 5, false, 5, 1f, 1));

        // Perm Flat Click
        UpgradeObjectDict.Add(22, new UpgradeObject("PFC1", 10, flatMultiplier, 5, true, 0, 6, 1));

        UpgradeObjectDict.Add(23, new PowerUp("PU1", 10, 10, false, 3000));

        numOfUpgrades = UpgradeObjectDict.Count;
        // TODO: Make sure to actually add the effects of every object that starts with a base level above 0 (Maybe for prestige only so move elsewhere?)
    }

    private bool checkValidId(int id)
    {
        //Debug.Log("ID " + id);
        if (UpgradeObjectDict.ContainsKey(id))
            return true;

        return false;
    }

    public UpgradeObject getUpgradeWithId(int id)
    {
        return UpgradeObjectDict[id];
    }

    private PowerUp getPowerUpWithId(int id)
    {
        return (PowerUp) UpgradeObjectDict[id];
    }

    private bool isPowerUp(int id)
    {
        if (getUpgradeWithId(id).GetType().Equals(typeof(PowerUp)))
            return true;
        return false;
    }
    // int id refers to the id of the chosen upgrade
    public void purchaseBuildingUpgrade(int id)
    {
        // Check if id is valid
        if (!checkValidId(id))
        {
            Debug.Log("Invalid ID " + id);
            return;
        }


        if (isPowerUp(id))
        {
            PowerUp obj = getPowerUpWithId(id);
            obj.purchase();
        } else
        {
            UpgradeObject obj = getUpgradeWithId(id);
            // Attempt to purchase the upgrade
            obj.purchase();

        }
        
        

        

    }

    public int getNumOfUpgrades()
    {
        return numOfUpgrades;
    }


}
