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
    { // TODO: Decide on the order of operations here
        stats.incrementMoney((long) (stats.getMoneyPerClick() * (1 + stats.getGlobalClickMultiplier() + stats.getPermanentClickMultiplier())) + stats.getPermanentClickIncrement());
    }

}
