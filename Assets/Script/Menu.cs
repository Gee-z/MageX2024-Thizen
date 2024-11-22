using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    
    public void StartGame()
    {
        AudioManager.instance.StopMusic("MainMenu");
        if(InventoryManager.instance != null)
        {
            InventoryManager.instance.Start = true;
        }
        PlayVideo();
        // FadeTransition.instance.ChangeScene(3);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public VideoPlayer videoPlayer;
    public SpriteRenderer Renderer;
    public GameObject BlackWhite;
    public GameObject Video;

    void Start()
    {
        if(InventoryManager.instance == null)
        {
            AudioManager.instance.PlayMusic("MainMenu");
        }
        // videoPlayer.Pause();
        // Register the event handler
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    void PlayVideo()
    {
        videoPlayer.Play();
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayer.Pause();
        Debug.Log("VideoCOmpleted");
        BlackWhite.SetActive(true);
        Video.SetActive(false);
        
        AudioManager.instance.PlayMusic("Hub");
        Renderer.DOFade(1f, 2f)
        .OnComplete(() =>
            DialogueManager.instance.StartDialog(0)
        );
        //VIdeoEnd
    }


    void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

}
