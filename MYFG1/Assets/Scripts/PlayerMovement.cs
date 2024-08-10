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

        
    }

    private void FixedUpdate()
    {
        if (isMoving(rb.position))
        {
            anim.SetFloat("Horizontal", Movedirection.x);
            anim.SetFloat("Vertical", Movedirection.y);
            anim.SetFloat("Speed", Movedirection.sqrMagnitude);
            move();
        }
    }

    private void move()
    {
        rb.MovePosition(rb.position + Movedirection * moveSpeed * Time.fixedDeltaTime);
    }

    private bool isMoving(Vector2 targetpos)
    {

        if(Physics2D.OverlapCircle(targetpos, 0.2f, InteractableLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
