using System.Collections;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string Name;

    [TextArea(3,0)]
    public Queue Sentences;
}