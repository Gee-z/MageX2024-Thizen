using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public int MissionIndex;
    public int ObjectiveIndex;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MissionManager.instance.ObjectiveComplete(MissionIndex,ObjectiveIndex);
        }
    }
}
