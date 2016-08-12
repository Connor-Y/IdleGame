using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickerController : MonoBehaviour {

    private StatTracker stats;
	// Use this for initialization
	void Awake () {
        stats = StatTracker.Instance;
    }

    public void buttonPressed()
    {
        stats.buttonPressed();
    }
    
}
