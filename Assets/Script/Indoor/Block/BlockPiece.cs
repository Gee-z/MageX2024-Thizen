using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BlockPiece : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;
    public GameObject Slot;
    public Block Block;
    public bool isAblock = false;

    void Start()
    {
        mainCamera = Camera.main; 
        transform.rotation = Quaternion.Euler(0,0, Random.Range(-40f, 40f));
    }

    void OnMouseDown()
    {
        Block.RenewPieceOrder(transform);
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        isDragging = true;
        transform.rotation = Quaternion.Euler(0,0,0);
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
        isDragging = false;
        Vector3 targetScale  = new Vector3 (1f,1f,1f);
        transform.DOScale(targetScale, 0.2f).SetEase(Ease.InOutQuad);
        if(Slot!=null && !isAblock)
        {
            transform.position = Slot.transform.position;
            Block.CheckCompletion();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0, Random.Range(-40f, 40f));
        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition; 
        mouseScreenPosition.z = 0;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Slot"))
        {
            Slot = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Slot"))
        {
            Slot = null;
        }
    }
}
