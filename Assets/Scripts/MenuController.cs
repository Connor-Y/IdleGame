using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {


    private bool isShowing;
    public GameObject upgradeMenuPanel;

    // Use this for initialization
    void Start () {
        isShowing = false;

    }
	

	// Update is called once per frame
	void Update () {
	
	}

    public void activateMenu()
    {
        if (isShowing)
        {
            isShowing = false;
            upgradeMenuPanel.SetActive(false);
            return;
        }

        isShowing = true;
        upgradeMenuPanel.SetActive(true);
    }
}
