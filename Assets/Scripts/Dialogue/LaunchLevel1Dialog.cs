using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LaunchLevel1Dialog : MonoBehaviour
{
    DialogTrigger dialogTrigger;

    Dialog dialog;

    public Text text;

    private float DialogRange = 1f;

    public GameObject LeBiscuit;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        dialogTrigger = GetComponent<DialogTrigger>();
        dialog = new Dialog{ Name = "Zerator le 10E" };
        dialog.Sentences = new Queue();
        dialog.Sentences.Enqueue("Bienvenue à toi jeune streamer!");
        dialog.Sentences.Enqueue("Alors comme àça tu crois que toi, Le Gros DUDU tu peux devenir le roi du Twitch game?");
        dialog.Sentences.Enqueue("Tu vas devoir te frayer un chemin dans l'immense manoir que je me suis payé avec l'argent des SUB MOUAHAHAHAH");
        dialogTrigger.TriggerDialog(dialog);
    }

    // Update is called once per frame
    void Update()
    {
        var LeBiscuitToTrigger = Vector2.Distance(LeBiscuit.transform.position, Player.transform.position);
        if(LeBiscuitToTrigger<DialogRange)
        {
            dialogTrigger = GetComponent<DialogTrigger>();
            dialog = new Dialog { Name = "Le vendeur" };
            dialog.Sentences = new Queue();
            dialog.Sentences.Enqueue("Ah un nouveau!");
            dialog.Sentences.Enqueue("J'ai rien à te refiler pour offir aux viewers pour l'instant, vu que t'es un noname");
            dialog.Sentences.Enqueue("Mais repasse plus tard j'aurais plein de trucs pour toi!");
            dialogTrigger.TriggerDialog(dialog);
        }
    }
}
