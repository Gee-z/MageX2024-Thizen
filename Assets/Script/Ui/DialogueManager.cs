using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {   
        public string name;
        [TextArea(3,10)]
        public string sentences;
        public bool Right;
        public int SpriteIndex;
    }
    [System.Serializable]
    public class Variant
    {
        public Dialogue[] dialog;
        public int[] charSpeak;
    }
    public TMP_Text dialogueText;
    public TMP_Text NameDisplay;
    public Queue<string> nameOrder;
    public Queue<string> sentences;
    public Queue<bool> positionQueue;
    public Queue<int> SpriteIndexQueue;
    public Variant[] DialogueArray;
    private Coroutine myCoroutine;
    public Sprite[] CharSprite;
    public SpriteRenderer LeftRenderer;
    public SpriteRenderer RightRenderer;
    public Color SpeakColor;
    public Color AfkColor;
    public GameObject ClickToContinue;
    public GameObject DialogueGo;
    public static DialogueManager instance;
    public bool isDialogue;
    public bool isatHUb;
    public bool isIntroScene = false;
    public GameObject Square;
    public GameObject Square2;
    public bool FinalDialogue;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        LeftRenderer.sprite = null;
        RightRenderer.sprite = null;
        nameOrder = new Queue<string>();
        sentences = new Queue<string>();
        positionQueue = new Queue<bool>();
        SpriteIndexQueue = new Queue<int>();
    }
    void Start()
    {
        if(InventoryManager.instance != null)
        {
            if ((InventoryManager.instance.CheckItem(1) && InventoryManager.instance.CheckItem(2) && InventoryManager.instance.CheckItem(3)) || InventoryManager.instance.CheckItem(4))
            {
                
            }
            else if (!isatHUb && InventoryManager.instance.Start)
            {
                if(dialogueState.instance.CheckVarUpdate("First Time Start"))
                    StartDialog(0);
            }
        }
        else if(!isIntroScene)
        {
            Debug.Log("Starting Dialogue");
            StartDialog(0);
        }
    }
    bool Clicked = false;
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& !Clicked)
        {
            Clicked = true;
        }
        if(Input.GetMouseButtonUp(0)&& Clicked)
        {
            Clicked = false;
        }
    }
    public void StartDialog(int index)
    {
        StartDialogue(DialogueArray[index]);
    }
   
    public void StartDialogue(Variant dialogVariant)
    {
        isDialogue = true;
        DialogueGo.SetActive(true);
        Dialogue[] dialogues = dialogVariant.dialog;
        if(myCoroutine != null){
            StopCoroutine(myCoroutine);
        }
        if(dialogVariant.charSpeak.Length == 2)
        {
            LeftRenderer.sprite = CharSprite[dialogVariant.charSpeak[0]];
            RightRenderer.sprite = CharSprite[dialogVariant.charSpeak[1]];
        }
        else{
            LeftRenderer.sprite = CharSprite[dialogVariant.charSpeak[0]];
            RightRenderer.sprite =null;
        }
        nameOrder.Clear();
        sentences.Clear();
        positionQueue.Clear();
        SpriteIndexQueue.Clear();
        foreach(Dialogue dialog in dialogues)
        {
            nameOrder.Enqueue(dialog.name);
            sentences.Enqueue(dialog.sentences);
            positionQueue.Enqueue(dialog.Right);
            SpriteIndexQueue.Enqueue(dialog.SpriteIndex);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence(){
        dialogueText.text = "";
        if(sentences.Count == 0){
            dialogueText.text = "";
            NameDisplay.text = "";
            isDialogue = false;
            
            if(isIntroScene)
            {
                FadeTransition.instance.ChangeScene(3);
            }
            if(FinalDialogue)
            {
                DialogueGo.GetComponent<SpriteRenderer>().sprite = null;
                StartCoroutine(ToBeContinuedAnim()); 
            }
            else
            {
                DialogueGo.SetActive(false);
            }
            return;
        }
        LeftRenderer.color = AfkColor;
        RightRenderer.color = AfkColor;
        if(positionQueue.Dequeue()) 
        {
            RightRenderer.sprite = CharSprite[SpriteIndexQueue.Dequeue()];
            RightRenderer.color  =  SpeakColor;
        }
        else 
        {
            LeftRenderer.sprite = CharSprite[SpriteIndexQueue.Dequeue()];
            LeftRenderer.color = SpeakColor;
        }
        
        string name = nameOrder.Dequeue();
        string sentence = sentences.Dequeue();
        myCoroutine = StartCoroutine(TypeSentence(sentence,name));
    }

    IEnumerator TypeSentence (string sentence, string name){
        yield return new WaitForSeconds(0.2f);
        NameDisplay.text = name;
        yield return new WaitForSeconds(0.1f);
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if(Clicked)
            {
                Clicked = false;
                dialogueText.text = sentence;
                break;
            } 
            yield return new WaitForSeconds(0.02f);
            if(letter == ',' || letter == '.')
            {
                yield return new WaitForSeconds(0.4f);
            }
        }
        ClickToContinue.SetActive(true);
        yield return new WaitUntil(() => Clicked);
        ClickToContinue.SetActive(false);
        DisplayNextSentence();
    }

    public GameObject ToBeContinued;
    IEnumerator ToBeContinuedAnim()
    {
        ToBeContinued.GetComponent<SpriteRenderer>().DOFade(1f, 3f);
        InventoryManager.instance.Start = false;
        yield return new WaitForSeconds(4f);
        yield return new WaitForSeconds(1f);
        FadeTransition.instance.ChangeScene(0);
    }
}