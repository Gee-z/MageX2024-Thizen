using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPiece : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;
    public Vector2 TargetPosition;
    public Crafting Crafting;

    void Start()
    {
        mainCamera = Camera.main; 
    }

    void OnMouseDown()
    {
        if(!Crafting.Completed)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            offset = transform.position - mousePosition;
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Crafting.CheckCompletion();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; 
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
