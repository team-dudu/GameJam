using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LaunchLevel1Dialog : MonoBehaviour
{
    DialogTrigger dialogTrigger;

    Dialog dialog;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        dialogTrigger = GetComponent<DialogTrigger>();
        dialog = new Dialog{ Name = "JICE" };
        dialog.Sentences = new Queue();
        dialog.Sentences.Enqueue("GreatJob");
        dialogTrigger.TriggerDialog(dialog);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
