using UnityEngine;
using System.Timers;

public class PowerUp : UpgradeObject
{

    // public UpgradeObject(string objectName, long baseCost, float baseCostMultiplier, long baseModifier, bool isClicker,
    //                      int maxLevel = 0, int upgradeType = 0, float baseMultiplier = 1f, int baseLevel = 0)

    private bool isActive;
    private Timer t;
    private int duration;

    public PowerUp(string objectName, long baseCost, long baseModifier, bool isClicker, int duration, int upgradeType = 0, float baseMultiplier = 1f, bool isActive = false) : base(objectName, baseCost, 1, baseModifier, isClicker, upgradeType, baseMultiplier)
    {
        this.isActive = isActive;
        this.duration = duration;
    }

    public void deactivePowerup()
    {
        isActive = !isActive;
        UpgradeObject revert = new UpgradeObject(getName() + " Reverter", 0, 1, (int) -1 * getModifier(), isClikerUpgrade(), getUpgradeType(), -1 * getMultiplier());
        revert.purchase();
        reset();

    }

    public new bool purchase()
    {
        Debug.Log("Purchasing Powerup");
        if (isActive)
        {
            Debug.Log("Can't Purchase active powerup again");
            // TODO: Handle Active powerup here
            return false;
        } else
        {
            isActive = true;
            base.purchase();
            t = new Timer();
            t.Elapsed += new ElapsedEventHandler(onTimeOut);
            t.Interval = duration;
            t.Start();
            return true;
        }
    }

    private void onTimeOut(object source, ElapsedEventArgs e)
    {
        t.Stop();
        deactivePowerup();
    }

}
