using UnityEngine;

public class DragCamera : MonoBehaviour
{
    public float dragSpeed = 7.5f;
    private Vector3 dragOrigin;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = -Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        transform.position += dragSpeed*(transform.up*pos.y + transform.right*pos.x);
        dragOrigin = Input.mousePosition;
    }


}
