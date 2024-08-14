using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogScript : MonoBehaviour
{
    [SerializeField] List<string> dialog;

    public List<string> Dialog
    {
        get { return dialog; }
    }
}   
