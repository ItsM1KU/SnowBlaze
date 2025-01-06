using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyItem : MonoBehaviour
{
    [SerializeField] moveBase itemMove;

    [SerializeField] int itemCost;

    [SerializeField] PlayerData playerData;

    [SerializeField] Button button;

    [SerializeField] GameObject soldSprite;
    public void buyMove()
    {   
        if(playerData.exp >= itemCost)
        {
            playerData.addMove(itemMove);
            playerData.updateExp(-itemCost);
            button.enabled = false;
            soldSprite.SetActive(true);
        }
        else
        {
            Debug.Log("not enough coins");
        }
    }

}
