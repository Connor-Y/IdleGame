using UnityEngine;
using System.Collections;

public class ToggleActive : MonoBehaviour {

    public bool isActive;
    private bool isVisible;

    /*
    public ToggleActive(bool isActive = true, bool isVisible = true)
    {
        this.isActive = isActive;
        this.isVisible = isVisible;
    } */

    void Start()
    {
        Debug.Log("Toggle Start");
        isVisible = isActive;
    }

    public void toggleActive()
    {
        Debug.Log("Toggle Active - Active: " + !isActive);
        isActive = !isActive;
        isVisible = !isVisible;
        gameObject.SetActive(isActive);
    }
 
}
