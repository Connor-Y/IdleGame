using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickerController : Singleton<ClickerController> {

    private ClickerController() { }

    private StatTracker stats;
	// Use this for initialization
	void Awake () {
        stats = StatTracker.Instance;
    }

    public void buttonPressed()
    {
        stats.incrementMoney(stats.getMoneyPerClick());
    }

}
