using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement; 
public class FadeTransition : MonoBehaviour
{
    public SpriteRenderer Renderer; 
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float delayBetweenFades = 1f; 
    public static FadeTransition instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }
    [ContextMenu("Test")]
    public void Fade()
    {
        StartCoroutine(FadeInAndOut());
    }
    IEnumerator FadeInAndOut()
    {
        yield return Renderer.DOFade(1f, fadeInDuration).WaitForCompletion();
        yield return new WaitForSeconds(delayBetweenFades);                 
        yield return Renderer.DOFade(0f, fadeOutDuration).WaitForCompletion(); 
    }
    public void FadeIn()
    {
        Renderer.DOFade(1f, fadeInDuration);
    }
    public void FadeOut()
    {
        Renderer.DOFade(1f, fadeInDuration);
    }
    public void ChangeScene(int Index)
    {
        StartCoroutine(FadeInAndOut());
        StartCoroutine(ChangeSceneFade(Index));
    }
    IEnumerator ChangeSceneFade(int Index)
    {
        yield return new WaitForSeconds(fadeInDuration);
        SceneManager.LoadScene(Index); 
    }
}
