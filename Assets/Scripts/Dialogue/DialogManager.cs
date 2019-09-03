using GameJam;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public bool IsDialoging;

    private static DialogManager m_Instance = null;

    public GameObject DialogWindow;

    public GameObject canvas;
    public Text nameText;
    public Text dialogueText;

    public static DialogManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (DialogManager)FindObjectOfType(typeof(DialogManager));
                if (m_Instance == null)
                    m_Instance = (new GameObject(typeof(DialogManager).Name)).AddComponent<DialogManager>();
            }
            return m_Instance;
        }
    }
    public Queue<string> sentences;



    void Update()
    {
        if (sentences != null && IsDialoging)
        {
            if (Input.GetButtonDown("Submit"))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialog(Dialog dialog)
    {
        IsDialoging = true;
        DialogWindow.SetActive(true);

        if (sentences == null)
        {
            sentences = new Queue<string>();
        }

        Time.timeScale = 0;
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

        if (!IsDialoging)
        {
            IsDialoging = true;
        }

        if (canvas == null)
        {
            canvas = transform.parent.gameObject;
        }
        canvas.gameObject.SetActive(true);

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        IsDialoging = false;
        DialogWindow.SetActive(false);
        Time.timeScale = 1;
    }
}