using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject DialogBox;
    [SerializeField] Text DialogText;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void showdialog(DialogScript dialog)
    {
        DialogBox.SetActive(true);
        StartCoroutine(dialogDisplay(dialog.Dialog[0]));

    }

    public IEnumerator dialogDisplay(string lines)
    {
        DialogText.text = "";
        foreach(var letter in lines.ToCharArray())
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(1 / 30);
        }
    }
}
