using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogScript : MonoBehaviour
{
    [SerializeField] TMP_Text dialogText;

    [SerializeField] GameObject ActionSelector;

    [SerializeField] GameObject MoveSelector;

    [SerializeField] List<GameObject> MoveList = new List<GameObject>();

    [SerializeField] GameObject Concentration;
    public IEnumerator typeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }

    public void EnableDialog(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        ActionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled) 
    { 
        MoveSelector.SetActive(enabled);
        Concentration.SetActive(enabled);
    }

    public void SetMoves(List<moveBase> moves)
    {
        for (int i = 0; i < MoveList.Count; i++) 
        { 
            Button button = MoveList[i].GetComponent<Button>();

            if(i < moves.Count)
            {
                Transform moveName = MoveList[i].transform.Find("moveName");
                if(moveName != null)
                {
                    TMP_Text moveNameText = moveName.GetComponent<TMP_Text>();
                    if(moveNameText != null)
                    {
                        moveNameText.text = moves[i].MoveName;
                    }
                }
                Transform moveDamage = MoveList[i].transform.Find("moveDamage");
                if (moveDamage != null)
                {
                    TMP_Text moveDamageText = moveDamage.GetComponent<TMP_Text>();
                    if (moveDamageText != null)
                    {
                        moveDamageText.text = moves[i].MoveDamage.ToString() + " Damage";
                    }
                }
                Transform moveAccuracy = MoveList[i].transform.Find("moveAccuracy");
                if (moveName != null)
                {
                    TMP_Text moveAccuracyText = moveAccuracy.GetComponent<TMP_Text>();
                    if (moveAccuracyText != null)
                    {
                        moveAccuracyText.text = moves[i].MoveAccuracy.ToString() + " Accuracy";
                    }
                }
                if(button != null)
                {
                    button.onClick.RemoveAllListeners();
                    button.interactable = true;
                }
            }
            else
            {
                Transform moveName = MoveList[i].transform.Find("moveName");
                if (moveName != null)
                {
                    TMP_Text moveNameText = moveName.GetComponent<TMP_Text>();
                    if (moveNameText != null)
                    {
                        moveNameText.text = "";
                    }
                }

                Transform moveDamage = MoveList[i].transform.Find("moveDamage");
                if (moveDamage != null)
                {
                    TMP_Text moveDamageText = moveDamage.GetComponent<TMP_Text>();
                    if (moveDamageText != null)
                    {
                        moveDamageText.text = "";
                    }
                }

                Transform moveAccuracy = MoveList[i].transform.Find("moveAccuracy");
                if (moveAccuracy != null)
                {
                    TMP_Text moveAccuracyText = moveAccuracy.GetComponent<TMP_Text>();
                    if (moveAccuracyText != null)
                    {
                        moveAccuracyText.text = "";
                    }
                }
                if (button != null) 
                {
                    button.interactable = false;
                    button.onClick.RemoveAllListeners();
                }
            }
        }
    }
}
