using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    Vector2 Movedirection;

    [SerializeField] LayerMask InteractableLayer;


    void Update()
    {
        Movedirection.x = Input.GetAxisRaw("Horizontal");
        Movedirection.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", Movedirection.sqrMagnitude);

        if (Movedirection != Vector2.zero)
        {
            anim.SetFloat("Horizontal", Movedirection.x);
            anim.SetFloat("Vertical", Movedirection.y);    
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Physics2D.OverlapCircle(rb.position, 0.5f, InteractableLayer))
            {
                Debug.Log("Interacting with NPC!!");
            }
        }
    }

    private void FixedUpdate()
    { 
        move();   
    }

    private void move()
    {
        rb.MovePosition(rb.position + Movedirection * moveSpeed * Time.fixedDeltaTime);
    }

}
