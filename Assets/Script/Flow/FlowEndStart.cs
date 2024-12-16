using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowEndStart : MonoBehaviour
{
    public float rayDistance = 0f;     
    public LayerMask layerMask;  
    public int Color;
    public bool isPortal = false;
    public int portalIndex ;
    void Start()
    {
         AssignStarter();
    }

    void Update()
    {

    }
    void OnMouseDown()
    {
        if(!FlowManager.instance.isChanging)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            
        }
    }
    void AssignStarter()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance, layerMask);
        if (hit.collider != null)
        {
            if(!isPortal)
            {
                hit.collider.GetComponent<FlowPiece>().MakeStarter(Color);
                hit.collider.GetComponent<FlowPiece>().EndStartPoint = this;
            }
            else
            {
                hit.collider.GetComponent<FlowPiece>().MakePortal(portalIndex);
            }
        }
    }
}
