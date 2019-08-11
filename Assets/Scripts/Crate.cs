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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            openable = false;

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.AddWeaponToInventory(objectsRessourceData.Weapons[0]);
        }
    }

    private void OpenCrate()
    {
        throw new NotImplementedException();
    }
}