using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogScript : MonoBehaviour
{
    [TextArea][SerializeField] List<string> dialogA;
    [TextArea][SerializeField] List<string> dialogB;

    public bool firstinteraction = false;
    public List<string> DialogA
    {
        get { return dialogA; }
    }
    public List<string> DialogB
    {
        get { return dialogB; }
    }

    public List<string> getCurrentDialog()
    {
        return firstinteraction ? dialogB : dialogA;
    }
}
