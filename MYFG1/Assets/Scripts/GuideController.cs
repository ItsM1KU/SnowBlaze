using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour, Interactable
{
    [SerializeField] DialogScript dialog;
    public void interact()
    {
        DialogManager.Instance.showdialog(dialog);
    }
}
