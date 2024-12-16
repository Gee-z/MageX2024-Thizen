using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Screen : MonoBehaviour
{
    public string Password;
    public TMP_InputField inputField;
    public TMP_Text PlaceHolder;
    public Sprite[] Logo;
    public Sprite emptyBackground;
    public SpriteRenderer LogoRenderer;
    public SpriteRenderer Background;
    public GameObject Canvas;
    public GameObject Loading;
    public bool isOpened;
    public bool Completed;

    private Coroutine myCoroutine;
    // Update is called once per frame
   
    public void CheckPassword(string Input)
    {
        Debug.Log(Input);
        if(Password.ToLower() == Input.ToLower())
        {
            Debug.Log("Completed");
            Completed = true;
            DialogueManager.instance.StartDialog(14);
            myCoroutine = StartCoroutine(ShowPassword());
        }
        else if(Input.ToLower() == "thizen")
        {
            DialogueManager.instance.StartDialog(15);
            StartCoroutine(WrongPassword("Wrong Password"));
        }
        else if(Input.ToLower() == "sunib")
        {
            DialogueManager.instance.StartDialog(16);
            StartCoroutine(WrongPassword("Wrong Password"));
        }
        else
        {
            StartCoroutine(WrongPassword("Wrong Password"));
        }
    }
    public void Close()
    {
        isOpened = false;
        Background.gameObject.SetActive(false);
        if(myCoroutine!=null)
        StopCoroutine(myCoroutine);
    }
    public void Open()
    {
        Background.gameObject.SetActive(true);
        isOpened = true;
        if(Completed)
        {
            myCoroutine = StartCoroutine(ShowPassword());
        }
        else DialogueManager.instance.StartDialog(12);
    }
    IEnumerator WrongPassword(string message)
    {
        inputField.text = "";
        PlaceHolder.text = message;
        yield return new WaitForSeconds(3f);
        PlaceHolder.text = "Input Password";
    }
    IEnumerator ShowPassword()
    {
        LogoRenderer.gameObject.SetActive(true);
        LogoRenderer.sprite = null;
        Canvas.SetActive(false);
        Background.sprite = emptyBackground;
        int i=0;
        Loading.SetActive(true);
        yield return new WaitForSeconds(3f);
        Loading.SetActive(false);
        yield return new WaitForSeconds(1f);
        while(isOpened)
        {
            if(i > 4) i = 0;
            LogoRenderer.sprite = Logo[i];
            i++;
            yield return new WaitForSeconds(1f);
            LogoRenderer.sprite = null;
            yield return new WaitForSeconds(1f);
        }
    }
}
