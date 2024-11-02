using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("UI")]
    public GameObject Container;
    public GameObject MissionUIGo;
    public static MissionManager instance;
    void Awake()
    {
        instance = this;
        MissionUiSpawned.Add(MissionUIGo);
        ShowMission();
    }
    private int MissionSpawned = 1;
    public List<GameObject> MissionUiSpawned = new List<GameObject>();
    //UI
    void ShowMission()
    {
        for(int i = 0; i < MissionSpawned;i++)
        {
            MissionUiSpawned[i].SetActive(true);
            UpdateUi(MissionUiSpawned[i].GetComponent<MissionUi>(),i);
        }
        for(int i = MissionSpawned; i < missionTaken.Count;i++)
        {
            GameObject  MissionGameObject = Instantiate(MissionUIGo, Vector3.zero, Quaternion.identity, Container.transform);
            MissionGameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
            UpdateUi(MissionGameObject.GetComponent<MissionUi>(),i);
            MissionSpawned++;
            MissionUiSpawned.Add(MissionGameObject);
        }
        Container.GetComponent<SizeUpdater>().ChangeSize(missionTaken.Count);
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



    //Objective Complete
    void ObjectiveComplete(int MissionIndex,int ObjectiveIndex)
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
