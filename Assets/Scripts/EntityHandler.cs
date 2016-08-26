using UnityEngine;

public class EntityHandler : MonoBehaviour {

    private bool isActive = true;
    private bool isVisible = true;


    void Start()
    {
        isVisible = isActive;
    }

    public void toggleActive()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }
 
    public void setActive(bool x)
    {
        isActive = x;
    }

    public void setVisible(bool x)
    {
        isVisible = x;
    }

}
