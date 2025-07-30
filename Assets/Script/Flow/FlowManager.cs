
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowManager : MonoBehaviour
{
    [System.Serializable]
    public class FlowLocation
    {
        public FlowPiece[] Row;
    }
    public static FlowManager instance;
    public FlowLocation[] Collumn;
    public GameObject[] StartPoint;
    public GameObject[] Portal; 
    public GameObject flowGo; 
    public List<GameObject> SpawnedObject = new List<GameObject>();
    public UnityEvent checkCompletion;
    public int Color;
    public bool isChanging = false;
    public bool CanChangeMore = false;
    public int Counter;
    public int Target;
    public bool PlayingFlow;
    public bool isHoveringStartPoint;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(!isHoveringStartPoint && isChanging)
            {
                isChanging = false;
                Reset();
            }
            else if(isChanging)
            {
                isChanging = false;
                Color = -1;
            }
            CheckWin();
        }
        if(Counter >= Target && Target >0)
        {
            Completed();
        }
    }

    void Reset()
    {
        isChanging = false;
        for(int i=0;i<6;i++)
        {
            for (int j=0;j<6;j++)
            {
                if(Collumn[i].Row[j].Color == Color)
                {
                    Collumn[i].Row[j].MakeNormal();
                }
            }
        }
        Color = -1;
    }
    void CheckWin()
    {
        Counter = 0;
        checkCompletion.Invoke();
    }
    public GameObject Tutorial;

    [ContextMenu("Reset Stats")]
    public void SpawnLevel1()
    {
        flowGo.SetActive(true);
        Tutorial.SetActive(true);
    }
    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
        PlayingFlow = true;
        Target = 10;
        flowGo.SetActive(true);
        for(int i = 0;i < 6;i++)
        {
            for(int j = 0;j < 6;j++)
            {
                Collumn[i].Row[j].Row = j;
                Collumn[i].Row[j].Collumn = i;
                Collumn[i].Row[j].reset();
            }
        }
        SpawnedObject.Add(Instantiate(StartPoint[0], Collumn[1].Row[1].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[0], Collumn[2].Row[3].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[1], Collumn[2].Row[1].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[1], Collumn[4].Row[4].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[2], Collumn[1].Row[4].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[2], Collumn[3].Row[3].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[3], Collumn[0].Row[4].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[3], Collumn[5].Row[1].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[4], Collumn[5].Row[0].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(StartPoint[4], Collumn[0].Row[5].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(Portal[1], Collumn[0].Row[0].gameObject.transform.position, Quaternion.identity));
        SpawnedObject.Add(Instantiate(Portal[1], Collumn[5].Row[5].gameObject.transform.position, Quaternion.identity));
    }
    public bridgeController bridgeController;
    void Completed()
    {
        flowGo.SetActive(false);
        foreach(GameObject a in SpawnedObject)
        {
            Destroy(a);
        }
        SpawnedObject.Clear();
        if(PlayingFlow)
        {
            PlayingFlow = false;
            bridgeController.interactBridge();
            DialogueManager.instance.StartDialog(7);
        }
    }
    
}
