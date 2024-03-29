﻿using GameJam;
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

    public bool opened = false;

    private DialogManager dialogManager;

    public ObjectsRessourceData objectsRessourceData;

    private Animator animator;

    private PlayerController playerController;

    private AudioSource audioSource;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        dialogManager = gameObject.GetComponent<DialogManager>();
        objectsRessourceData = gameObject.GetComponent<IObjectsRessourceData>() as ObjectsRessourceData;
    }

    // Update is called once per frame
    void Update()
    {
        if(openable && Input.GetButtonDown("Submit") && !opened)
        {
            OpenCrate();
            opened = true;
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
        if (playerController != null && openable == true)
        {
            //audioSource.PlayOneShot(audioSource.clip);

            animator.SetAnimation(AnimationParameter.Open);

            playerController.AddWeaponToInventory(objectsRessourceData.Weapons[0]);

            Dialog dialog = new Dialog
            {
                Name = "Coffre",
                Sentences = new Queue()
            };

            dialog.Sentences.Enqueue("Tu viens de trouver " + objectsRessourceData.Weapons[0].name);
            DialogManager.Instance.StartDialog(dialog);
        }
    }
}