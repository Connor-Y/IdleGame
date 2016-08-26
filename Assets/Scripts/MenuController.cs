using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {


    private bool isShowing;
    public GameObject menuPanel;

    // Use this for initialization
    void Start () {
        isShowing = menuPanel.activeSelf;
    }
	

    public void activateMenu()
    {
        if (isShowing)
        {
            isShowing = false;
            menuPanel.SetActive(false);
            return;
        }

        isShowing = true;
        menuPanel.SetActive(true);
    }
}
