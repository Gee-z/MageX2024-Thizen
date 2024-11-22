using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public GameObject LongHand;
    public GameObject ShortHand;
    public GameObject Clock;
    public bool isSpinning;
    public bool Completed = false;
    public float LongHandTarget;
    public float ShortHandTarget;
    public float Offset;
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        isSpinning = true;
    }
    void OnMouseUp()
    {
        isSpinning = false;
        CheckValue();
    }
    private float lastTargetZRotation = 0f;
    private float cumulativeRotation = -4f * 3600f;
    public void Open()
    {
        Clock.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = true;
        if(!Completed)
        {
            DialogueManager.instance.StartDialog(2);
        }
    }
    public void Close()
    {
        Clock.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
    }
    void CheckValue()
    {
        if(!Completed)
        {
            if(LongHand.transform.eulerAngles.z > LongHandTarget - Offset && LongHand.transform.eulerAngles.z < LongHandTarget + Offset && ShortHand.transform.eulerAngles.z > ShortHandTarget - Offset && ShortHand.transform.eulerAngles.z < ShortHandTarget + Offset)
            {
                Debug.Log("Completed");
                Completed = true;
                DialogueManager.instance.StartDialog(3);
            }
        }
    }
    void Update()
    {
        if(isSpinning && !Completed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = mousePosition - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            LongHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90f));

            float currentTargetZRotation = LongHand.transform.eulerAngles.z;

            float deltaRotation = Mathf.DeltaAngle(lastTargetZRotation, currentTargetZRotation);

            cumulativeRotation += deltaRotation;

            ShortHand.transform.rotation = Quaternion.Euler(0, 0, cumulativeRotation  / 12f);

            lastTargetZRotation = currentTargetZRotation;
        }
    }
}
