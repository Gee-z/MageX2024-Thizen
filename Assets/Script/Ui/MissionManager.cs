using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [System.Serializable]
    public class MissionStruct
    {
        public string MissionName;
        public int ObjectiveCount = 3;
        public string[] ObjectiveDetail = new string[3];
        
        [HideInInspector]
        public bool[] objective = new bool[]{false,false,false};
        [HideInInspector]
        public bool Completed = false;
    }

    public MissionStruct[] Mission;
    public List<int> missionTaken = new List<int>();
    public List<int> missionCompleted = new List<int>();
    [Header("UI")]
    public GameObject ContainerOnprogress;
    public GameObject ContainerCompleted;
    public GameObject MissionUIGo;
    public GameObject EmptyWarning;
    public static MissionManager instance;
    void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Upadating isntances");
            instance = this;  
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
        EmptyWarning.SetActive(false);
    }
    public List<GameObject> MissionUiSpawned = new List<GameObject>();
    public List<GameObject> CompletedMissionUiSpawned = new List<GameObject>();
    public ScrollRect scrollRect;  

    public void CloseAllMissionUi()
    {
        ContainerOnprogress.SetActive(false);
        ContainerCompleted.SetActive(false);
        EmptyWarning.SetActive(false);
    }


    // [ContextMenu("Show On Progress Mission")]
    public void ShowMissionOnProgress()
    {
        CloseAllMissionUi();
        ContainerOnprogress.SetActive(true);
        scrollRect.content = ContainerOnprogress.GetComponent<RectTransform>();
        if(missionTaken.Count-missionCompleted.Count == 0) SetWarningToParent(ContainerOnprogress);
        for(int i = 0; i < MissionUiSpawned.Count;i++)
        {
            if(Mission[MissionUiSpawned[i].GetComponent<MissionUi>().MissionIndex].Completed)
            {
                missionCompleted.Add(MissionUiSpawned[i].GetComponent<MissionUi>().MissionIndex);
                Destroy(MissionUiSpawned[i]);
                MissionUiSpawned.RemoveAt(i);
            }
            else
            {
                UpdateUi(MissionUiSpawned[i].GetComponent<MissionUi>(),i);
            }
        }
        for(int i = MissionUiSpawned.Count; i < missionTaken.Count-missionCompleted.Count;i++)
        {
            GameObject  MissionGameObject = Instantiate(MissionUIGo, Vector3.zero, Quaternion.identity, ContainerOnprogress.transform);
            MissionGameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
            UpdateUi(MissionGameObject.GetComponent<MissionUi>(),i);
            MissionGameObject.GetComponent<MissionUi>().MissionIndex = missionTaken[i];
            MissionUiSpawned.Add(MissionGameObject);
        }
        ContainerOnprogress.GetComponent<SizeUpdater>().ChangeSize(missionTaken.Count-missionCompleted.Count);
    }


    // [ContextMenu("Show Completed Mission")]
    public void ShowMissionCompleted()
    {
        CloseAllMissionUi();
        ContainerCompleted.SetActive(true);
        scrollRect.content = ContainerCompleted.GetComponent<RectTransform>();
        if(missionCompleted.Count == 0) SetWarningToParent(ContainerCompleted);
        for(int i = 0; i < CompletedMissionUiSpawned.Count;i++)
        {
            UpdateUi(CompletedMissionUiSpawned[i].GetComponent<MissionUi>(),i);
        }
        for(int i = CompletedMissionUiSpawned.Count; i < missionCompleted.Count;i++)
        {
            GameObject  MissionGameObject = Instantiate(MissionUIGo, Vector3.zero, Quaternion.identity, ContainerCompleted.transform);
            MissionGameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
            UpdateUi(MissionGameObject.GetComponent<MissionUi>(),i);
            MissionGameObject.GetComponent<MissionUi>().MissionIndex = missionCompleted[i];
            CompletedMissionUiSpawned.Add(MissionGameObject);
        }
        ContainerCompleted.GetComponent<SizeUpdater>().ChangeSize(missionCompleted.Count);
    }

    void SetWarningToParent(GameObject parentObject)
    {
        EmptyWarning.transform.SetParent(parentObject.transform);
        EmptyWarning.SetActive(true);
    }


    void UpdateUi(MissionUi Target,int i)
    {
            Target.MissionName.text = Mission[i].MissionName;
            if(Mission[i].Completed) Target.MissionState.text = "Completed";
            else Target.MissionState.text ="On Progress";
            for(int j = 0; j < 3; j++)
            {
                Target.ObjectiveGo[j].SetActive(false);
            }
            for(int j = 0; j < Mission[i].ObjectiveCount ; j++)
            {
                Target.ObjectiveGo[j].SetActive(true);
                if(Mission[i].objective[j]) 
                {
                    Target.Objective[j].text = "<s>" + Mission[i].ObjectiveDetail[j] + "</s>";
                }
                
                else Target.Objective[j].text =  Mission[i].ObjectiveDetail[j];
            }
    }

    int C = 0;
    [ContextMenu("Complete")]
    void CompleteMission()
    {
        ObjectiveComplete(0,C);
        ShowMissionOnProgress();
        C++;
    }

    //Objective Complete
    public void ObjectiveComplete(int MissionIndex,int ObjectiveIndex)
    {
        Mission[MissionIndex].objective[ObjectiveIndex] = true;
        if(CheckAllObjective(MissionIndex))
        {
            Mission[MissionIndex].Completed = true;
        }
    }
    bool CheckAllObjective(int index)
    {
        int counter = 0;
        for(int i = 0;i < Mission[index].ObjectiveCount;i++)
        {
            if(Mission[index].objective[i]) counter++;
        }
        return counter == Mission[index].objective.Length;
    }
}
