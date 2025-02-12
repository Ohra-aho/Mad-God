using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    private Vector3 originalPosition;

    public bool destroyOnDrop;
    public bool resetOnDrop;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // Calculate the offset between the mouse position and the object's position
        offset = transform.position - GetMouseWorldPosition();
        originalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // Update the object's position to follow the mouse, adjusted by the offset
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        if (destroyOnDrop)
        {
            Destroy(this.gameObject);
        } else if(resetOnDrop)
        {
            transform.position = originalPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // Set Z to camera's distance
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
