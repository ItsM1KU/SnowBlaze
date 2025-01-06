using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgradeItem : MonoBehaviour
{
    [SerializeField] TMP_Text moveName;
    [SerializeField] TMP_Text currDmg;
    [SerializeField] TMP_Text upgDmg;
    [SerializeField] TMP_Text costText;
    [SerializeField] GameObject[] upgradeIcons;
    [SerializeField] Button upgButton;

    private PlayerData playerData;

    private moveBase move;
    private int cost;


    private void Start()
    {
        
    }
    public void setup(moveBase move)
    {
        this.move = move;
        playerData = GameObject.Find("Player").GetComponent<PlayerData>();
        moveName.text = move.MoveName;
        currDmg.text = move.MoveDamage.ToString();
        upgDmg.text = move.GetUpgradedDamage().ToString();
        
        if(move.CurrentUpgrade == 0)
        {
            cost = 5;
            costText.text = cost.ToString();
        }
        else
        {
            cost = 10;
            costText.text = cost.ToString();
        }


        for (int i = 0; i < upgradeIcons.Length; i++)
        {
            upgradeIcons[i].SetActive(i < move.CurrentUpgrade);
        }

        
        upgButton.interactable = move.canUpgrade() && (playerData.coins >= cost);
    }

    public void buttonPressed()
    {
        if (move.canUpgrade() && (playerData.coins >= cost))
        {
            move.upgrade();
            setup(move);
            playerData.DeductCoins(cost);
        }
        else
        {
            Debug.Log("play a error soundeffect");
        }
    }
    
}
