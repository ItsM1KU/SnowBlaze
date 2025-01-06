using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    Vector2 Movedirection;

    bool ismoving;

    [SerializeField] LayerMask InteractableLayer;
    [SerializeField] LayerMask BossLayer;
    [SerializeField] LayerMask BattleLayer;

    [SerializeField] float encounterCheckDistance = 3.0f;
    private Vector2 lastEncounterCheckPosition;

    [SerializeField] mobController mobController;

    public event Action onMobEncounter;
    public event Action<Collider2D> bossInteraction;
    
    public void HandleUpdate()
    {
        if (!ismoving)
        {
            Movedirection.x = Input.GetAxisRaw("Horizontal");
            Movedirection.y = Input.GetAxisRaw("Vertical");


            //to remove diagonal movement
            if (Movedirection.x != 0) Movedirection.y = 0;

            if (Movedirection != Vector2.zero)
            {
                anim.SetFloat("Horizontal", Movedirection.x);
                anim.SetFloat("Vertical", Movedirection.y);
                move();
                //StartCoroutine(moveP(targetpos));
                checkforEncounters();
            }
        }
        

        anim.SetFloat("Speed", Movedirection.sqrMagnitude);


        if(Input.GetKeyDown(KeyCode.E))
        {
            interact();
            bossInteract();
        }
    }

    IEnumerator moveP(Vector3 targetpos)
    {
        ismoving = true;
        while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetpos;
        ismoving = false;
    }

    void interact()
    {
        var collider = Physics2D.OverlapCircle(rb.position, 0.5f, InteractableLayer);
        if(collider != null)
        {
            anim.SetFloat("Speed", 0);
            collider.GetComponent<Interactable>()?.interact();
        }
    }

    private void FixedUpdate()
    { 
        //move();   
    }

    private void move()
    {
        ismoving = true;
        rb.MovePosition(rb.position + Movedirection * moveSpeed * Time.fixedDeltaTime);
        ismoving = false;
    }

    void checkforEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.01f, BattleLayer))
        {
            float distanceMoved = Vector2.Distance(lastEncounterCheckPosition, transform.position);
            if(distanceMoved > encounterCheckDistance)
            {
                lastEncounterCheckPosition = transform.position;

                if (UnityEngine.Random.Range(1, 100) <= 5)
                {
                    anim.SetFloat("Speed", 0);
                    mobController.SpawnMob();
                    onMobEncounter?.Invoke();
                }
            }
            
        }
    }


    void bossInteract()
    {
        var collider = Physics2D.OverlapCircle(rb.position, 0.5f, BossLayer);
        if (collider != null)
        {
            anim.SetFloat("Speed", 0);
            Debug.Log("it workds");
            //collider.GetComponent<Interactable>()?.interact();
            bossInteraction?.Invoke(collider);
        }
    }

    void checkforBoss()
    {
        //i check if enemy is close
        //i will return a event action to start the boss battle which can happen if the player chose to 
    }

}
