using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Gamestates { Freeroam, Dialog, Fight}
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    Gamestates gamestate;

    private void Update()
    {
        if(gamestate == Gamestates.Freeroam)
        {
            playerMovement.HandleUpdate();
        }
        else if(gamestate == Gamestates.Dialog)
        {

        }
        else if (gamestate == Gamestates.Fight)
        {

        }
    }
}