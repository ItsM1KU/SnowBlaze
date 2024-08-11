using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            interact();
        }
    }

    void interact()
    {
        var collider = Physics2D.OverlapCircle(rb.position, 0.5f, InteractableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.interact();
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
