using GameJam;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Crate : MonoBehaviour
{
    public bool openable = false;

    private DialogManager dialogManager = new DialogManager();

    public ObjectsRessourceData objectsRessourceData = new ObjectsRessourceData();
    
    private string gameDataFileName = "Objects.json";

    private PlayerController playerController;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(openable && Input.GetButtonDown("Submit"))
        {
            OpenCrate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            openable = true;

            playerController = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            openable = false;
        }
    }

    private void OpenCrate()
    {
        if(playerController!=null)
        {
            playerController.AddWeaponToInventory(objectsRessourceData.Weapons[0]);

            Dialog dialog = new Dialog
            {
                Name = "Coffre",
                Sentences = new Queue()
            };

            dialog.Sentences.Enqueue("Tu viens de trouver " + objectsRessourceData.Weapons[0].name);

            dialogManager.StartDialog(dialog);
        }
    }
}