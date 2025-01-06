using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChallengerController : MonoBehaviour
{
    [SerializeField] MobBase bossMob;
    [SerializeField] enemyUnit enemyBattleUnit;

    [SerializeField] GameObject dialogBox;
    [SerializeField] Image chalAvatar;
    [SerializeField] Sprite chalSprite;
    [SerializeField] Text dialogText;

    [SerializeField] string[] dialogScript;
    int currentindex = 0;

    public IEnumerator BattleIntro()
    {
        dialogText.text = "";
        chalAvatar.sprite = chalSprite;
        dialogBox.SetActive(true);
        for (int i = 0; i < dialogScript.Length; i++) 
        {
            dialogText.text = "";
            StartCoroutine(bossdialog(dialogScript[currentindex]));
            currentindex++;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        Debug.Log("Batte starts");
        enemyBattleUnit.assignMob(bossMob);
        Debug.Log($" assigned {enemyBattleUnit.mobBase}");
        dialogBox.SetActive(false);
    }


    public IEnumerator bossdialog(string dialog)
    {
        foreach(char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 60);
        }
    }

}
