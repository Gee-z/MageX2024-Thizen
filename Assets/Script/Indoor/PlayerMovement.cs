using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;
    public float Negative;
    public float Normal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Negative = spriteRenderer.gameObject.transform.localScale.x * -1f;
        Normal = spriteRenderer.gameObject.transform.localScale.x ;
    }
    public Animator anim;
    void Update()
    {
        if(!DialogueManager.instance.isDialogue)
        {
            if(PuzzleManager.instance != null)
            {
                if(!PuzzleManager.instance.OpeningUi ) movement.x = Input.GetAxisRaw("Horizontal");
                else movement.x = 0f;
            }
            else
            {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            rb.velocity = new Vector2(movement.x * moveSpeed,0f);
            if(movement.x == 0)
                anim.SetBool("Running", false);
            else
                anim.SetBool("Running", true);

            if (movement.x > 0)
            {
                spriteRenderer.gameObject.transform.localScale = new Vector2(Normal,spriteRenderer.gameObject.transform.localScale.y);
            }
            else if (movement.x < 0)
            {
                spriteRenderer.gameObject.transform.localScale = new Vector2(Negative,spriteRenderer.gameObject.transform.localScale.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0,0f);
        }
    }

}
