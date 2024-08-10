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
