using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Queue<string> sentences;

    public Canvas canvas;
    public Text nameText;
    public Text dialogueText;

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog)
    {

        if(sentences== null)
        {
            sentences = new Queue<string>();
        }

        nameText.text = dialog.Name;
        //Debug.Log("Starting conversation with " + dialog.Name);

        sentences.Clear();

        foreach (string sentence in dialog.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //TODO add déclenchement étape suivante sur la touche e
    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
        //Debug.Log(sentence);
    }

    private void EndDialogue()
    {
        canvas.gameObject.SetActive(false);
        Debug.Log("End of dialog");
    }
}
