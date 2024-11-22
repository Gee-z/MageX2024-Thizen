using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main; 
    }

    void OnMouseDown()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        isDragging = true;
        Vector3 targetScale  = new Vector3 (1.5f,1.5f,1.5f);
        transform.DOScale(targetScale, 0.2f).SetEase(Ease.InOutQuad);
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
        Sequence sequence = DOTween.Sequence();
        Vector3 targetScale  = new Vector3 (1f,1f,1f);
        sequence.Append(transform.DOScale(targetScale, 0.2f).SetEase(Ease.InOutQuad));
        Vector3 targetPosition = new Vector3(0f,0f,0.66f);
        sequence.Join(transform.DOLocalMove(targetPosition, 0.2f).SetEase(Ease.InOutQuad));
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; 
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
