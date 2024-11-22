using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; 

public class PuzzleManager : MonoBehaviour
{

    public static PuzzleManager instance;
    public ClockPuzzle clockPuzzle;
    public ClockDrawer clockDrawer;
    public Safe safe;
    public GameObject RightScreen;
    public GameObject News;
    public GameObject BookShelf;
    public Screen Screen;
    public GameObject Dark;
    public GameObject Paper;
    public Block block;
    public TableDrawer TableDrawer;

    public bool OpeningUi = false;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DialogueManager.instance.StartDialog(0);
    }
    public void OpenClock()
    {
        Dark.SetActive(true);
        Debug.Log("Open Clock");
        OpeningUi = true;
        clockPuzzle.Open();
        clockDrawer.Close();
    }
    public void OpenDrawer()
    {
        Dark.SetActive(true);
        Debug.Log("Open Drawer");
        OpeningUi = true;
        clockPuzzle.Close();
        clockDrawer.Open();
    }
    public void CloseClock()
    {
        Dark.SetActive(false);
        OpeningUi = false;
        clockPuzzle.Close();
        clockDrawer.Close();
    }
    public void OpenSafe()
    {
        Dark.SetActive(true);
        OpeningUi = true;
        safe.Open();
    }
    public void CloseSafe()
    {
        OpeningUi = false;
        Dark.SetActive(false);
        safe.Close();
    } 
    public void OpenBookShelf()
    {
        DialogueManager.instance.StartDialog(19);
        Dark.SetActive(true);
        OpeningUi = true;
        BookShelf.SetActive(true);
    }  
    public void CloseBookShelf()
    {
        Dark.SetActive(false);
        OpeningUi = false;
        BookShelf.SetActive(false);
    }

    public void OpenScreen()
    {
        Dark.SetActive(true);
        News.SetActive(false);
        RightScreen.SetActive(false);
        OpeningUi = true;
        Screen.Open();
    }
    public void CloseScreen()
    {
        Dark.SetActive(false);
        OpeningUi = false;
        Screen.Close();
        News.SetActive(false);
        RightScreen.SetActive(false);
    }
    public void OpenNews()
    {
        DialogueManager.instance.StartDialog(11);
        Dark.SetActive(true);
        Screen.Close();
        News.SetActive(true);
    }
    public void OpenSideScreen()
    {
        DialogueManager.instance.StartDialog(13);
        Dark.SetActive(true);
        Screen.Close();
        RightScreen.SetActive(true);
    }
    public void OpenPaper()
    {
        DialogueManager.instance.StartDialog(1);
        OpeningUi = true;
        Dark.SetActive(true);
        Paper.SetActive(true);
    }
    public void ClosePaper()
    {
        OpeningUi = false;
        Dark.SetActive(false);
        Paper.SetActive(false);
    }
    public void OpenBlock()
    {
        OpeningUi = true;
        Dark.SetActive(true);
        block.Open();
    }
    public void CloseBlock()
    {
        OpeningUi = false;
        Dark.SetActive(false);
        block.Close();
    }
    public void OpenTableDrawer()
    {
        OpeningUi = true;
        Dark.SetActive(true);
        TableDrawer.Open();
    }
    public void CloseTableDrawer()
    {
        OpeningUi = false;
        Dark.SetActive(false);
        TableDrawer.Close();
    }
    public void EnterLift()
    {
        if(InventoryManager.instance.CheckItem(1) && InventoryManager.instance.CheckItem(2) && InventoryManager.instance.CheckItem(3))
        {
            AudioManager.instance.ChangeMusic("Underground","Overworld");
            FadeTransition.instance.ChangeScene(1);
        }
        else
        {
            DialogueManager.instance.StartDialog(20);
        }
    }
    public void AddItemsDrill3(GameObject Drill)
    {
        InventoryManager.instance.AddItem(0);
        InventoryManager.instance.AddItem(3);
        Drill.SetActive(false);
        CheckCompleted();
    }
    public void AddItemsDrill1(GameObject Drill)
    {
        InventoryManager.instance.AddItem(1);
        Drill.SetActive(false);
        CheckCompleted();
    }
    public void AddItemsDrill2(GameObject Drill)
    {
        InventoryManager.instance.AddItem(2);
        Drill.SetActive(false);
        CheckCompleted();
    }
    public void CheckCompleted()
    {
        if(InventoryManager.instance.CheckItem(1)&&InventoryManager.instance.CheckItem(2)&&InventoryManager.instance.CheckItem(3))
        {
            DialogueManager.instance.StartDialog(21);
        }
    }
}
