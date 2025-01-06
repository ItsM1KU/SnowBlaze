using System;
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

    public event Action onShowDialog;
    public event Action onHideDialog;

    private bool isTyping;
    DialogScript dialog;
    int currentLine = 0;

    private List<string> currentDialog;

    private void Awake()
    {
        Instance = this;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping) 
        {
            ++currentLine;
            if(currentLine < currentDialog.Count)
            {
                StartCoroutine(dialogDisplay(currentDialog[currentLine]));
            }
            else
            {
                DialogBox.SetActive(false);
                currentLine = 0;
                onHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator showdialog(DialogScript dialog)
    {
        yield return new WaitForEndOfFrame();
        onShowDialog?.Invoke();
        this.dialog = dialog;
        DialogBox.SetActive(true);

        currentDialog = dialog.getCurrentDialog();
        StartCoroutine(dialogDisplay(currentDialog[0]));

        if(currentDialog == dialog.DialogA)
        {
            dialog.firstinteraction = true;
        }
    }

    public IEnumerator dialogDisplay(string lines)
    {
        isTyping = true;
        DialogText.text = "";
        foreach(var letter in lines.ToCharArray())
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(1 / 60);
        }
        isTyping = false;
    }
}
