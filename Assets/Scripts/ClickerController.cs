using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickerController : MonoBehaviour {

    private int money;
    public Text moneyText;
    private int moneyPerClick;

	// Use this for initialization
	void Start () {
        money = 0;
        updateMoneyText();
        moneyPerClick = 1;

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void updateMoneyText()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public void buttonPressed()
    {
        money += moneyPerClick;
        updateMoneyText();
    }
    
}
