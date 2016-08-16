using UnityEngine;
using System.Collections;

public class SingleUseUpgrades : UpgradeObject
{

    private float multiplier;
    private long flatIncrement;
    private bool alreadyPurchased;

    // public UpgradeObject (string objectName, long baseCost, float baseMultiplier, long baseModifier, bool isClicker, int baseLevel = 0)
    public SingleUseUpgrades(string objectName, long baseCost, bool isClicker, int upgradeType, bool isShowing = true, float multiplier = 1f, long flatIncrement = 0) : base(objectName, baseCost, 1, 0, isClicker, upgradeType, isShowing)
    {
        this.multiplier = multiplier;
        this.flatIncrement = flatIncrement;
        alreadyPurchased = false;
}
    
    public new float getMultiplier()
    {
        return multiplier;
    }

    public long getFlatIncrement()
    {
        return flatIncrement;
    }

    public bool getPurchased()
    {
        return alreadyPurchased;
    }

    public new void purchase()
    {

        if (alreadyPurchased)
        {
            Debug.Log("Already Purchased");
            return;
        }

        if (base.purchase())
        {
            alreadyPurchased = true;
            //toggleVisible(); // TODO: Decide whether we want to hide purchased upgrades or leave them showing (showing prob better) (for now they hide)
        }



    }


}
