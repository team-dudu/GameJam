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
        if(Input.GetButtonDown("Submit"))
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

        sentences.Clear();

        foreach (string sentence in dialog.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        canvas.gameObject.SetActive(false);
    }
}
