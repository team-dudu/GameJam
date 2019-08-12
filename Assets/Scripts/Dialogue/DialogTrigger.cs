using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void TriggerDialog(Dialog dialog)
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
