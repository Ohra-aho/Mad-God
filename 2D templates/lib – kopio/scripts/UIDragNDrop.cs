using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    public bool destroyOnDrop;
    public bool resetOnDrop;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Find the Canvas component (or assign manually if needed)
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found. Make sure this script is attached to a UI element within a Canvas.");
        }

        // Store the initial position
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the UI element with the pointer, adjusted for the canvas scale
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(destroyOnDrop)
        {
            Destroy(this.gameObject);
        }else if(resetOnDrop)
        {
            ResetPosition();
        }
    }

    // Optional: Add a method to reset position manually if needed
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }


}
