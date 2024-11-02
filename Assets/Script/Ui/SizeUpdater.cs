using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUpdater : MonoBehaviour
{
    public float BoxSize;

    
    [ContextMenu("Change Size")]
    public void ChangeSize(int i)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float a = (i * BoxSize)+(234.8f-BoxSize);
        if (rectTransform  != null)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, a);
        }
        rectTransform.localPosition = new Vector2(5f,-150f);
    }
}
