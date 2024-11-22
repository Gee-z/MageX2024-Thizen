using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    
    public TMP_Text LeftMenuTitle;
    public GameObject Menu;
    private bool isOpened = false;
    [Header("Mission")]
    public GameObject MissionButton1;
    public GameObject MissionButton2;
    public static MenuManager instance;

    public bool NewRotation = false;
    public float SavedRotation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Menu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
    public void OpenMission()
    {
        CloseAllUi();
        LeftMenuTitle.text = "Missions";
        MissionManager.instance.ShowMissionOnProgress();
        MissionButton1.GetComponent<Animator>().SetTrigger("Normal");
        MissionButton2.GetComponent<Animator>().SetTrigger("Normal");
        MissionButton1.GetComponent<Animator>().SetTrigger("Selected");
    }
    public void CloseMission()
    {
        MissionManager.instance.CloseAllMissionUi();
        return;
    }
    public void CloseAllUi()
    {
        LeftMenuTitle.text = "";
        CloseMission();
        InventoryManager.instance.CloseInventory();
        return;
    }

    public void OpenInventory()
    {
        CloseAllUi();
        MissionButton1.GetComponent<Animator>().SetTrigger("Close");
        MissionButton2.GetComponent<Animator>().SetTrigger("Close");
        LeftMenuTitle.text = "Inventory";
        InventoryManager.instance.OpenInventory();
    }

    public void OpenPlanet()
    {
        CloseAllUi();
        MissionButton1.GetComponent<Animator>().SetTrigger("Close");
        MissionButton2.GetComponent<Animator>().SetTrigger("Close");
        LeftMenuTitle.text = "Planet";
    }

    [ContextMenu("Open Menu")]
    public void OpenMenu()
    {
        if(!isOpened)
        {
            Menu.SetActive(true);
            OpenMission();
        }
        else 
            Menu.SetActive(false);
            
        isOpened = !isOpened;
    }
}
