using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public Transform[] Pieces;
    public Transform[] Slot;
    public GameObject Glow;
    public GameObject BlockPuzzle;
    public bool Completed = false;
    List<Transform> pieceList = new List<Transform>();

    void Start()
    {
        UpdateRenderOrder();
        pieceList = new List<Transform>(Pieces);
    }
    public void Open()
    {
        BlockPuzzle.SetActive(true);
        if (!Completed) DialogueManager.instance.StartDialog(7);
    }
    public void Close()
    {
        BlockPuzzle.SetActive(false);
    }
    public void CheckCompletion()
    {
        if (Pieces[0].position == Slot[0].position && Pieces[1].position == Slot[1].position && Pieces[2].position == Slot[2].position && Pieces[3].position == Slot[3].position && Pieces[4].position == Slot[4].position)
        {
            StartCoroutine(Unfade());
            Completed = true;
            DialogueManager.instance.StartDialog(8);
        }
    }
    IEnumerator Unfade()
    {
        Glow.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < 5; i++)
        {
            Pieces[i].gameObject.SetActive(false);
            Slot[i].gameObject.SetActive(false);
        }
    }
    public void RenewPieceOrder(Transform trans)
    {
        if (pieceList.Contains(trans))
        {
            pieceList.Remove(trans);
            pieceList.Add(trans);
        }
        UpdateRenderOrder();
    }

    public void UpdateRenderOrder()
    {
        for (int i = 0; i < pieceList.Count; i++)
        {
            SpriteRenderer sr = pieceList[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = i + 1;
            }
        }
    }

}
