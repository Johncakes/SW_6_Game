using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNodeInRange : MonoBehaviour
{
    bool hasNote = false;
    bool attack = false;
    private FloorChecker floorChecker;
    public bool playerInRange = false;

    private bool left = false;
    private bool right = false;
    private bool up = false;
    private bool down = false;
    private bool deleteNote = false;

    void Start()
    {
        floorChecker = GetComponentInChildren<FloorChecker>();
    }

    public void OnChildTrigger()
    {
        if (floorChecker != null)
        {
            playerInRange = floorChecker.hasPlayer;
        }
    }

    void Update()
    {

        if (hasNote)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                attack = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                up = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                down = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {

            hasNote = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            Note note = other.gameObject.GetComponent<Note>();
            if (attack && playerInRange)
            {
                if (note.isLeft && left)
                {
                    deleteNote = true;
                }
                if (note.isRight && right)
                {
                    deleteNote = true;
                }
                if (note.isUp && up)
                {
                    deleteNote = true;
                }
                if (note.isDown && down)
                {
                    deleteNote = true;
                }
            }
            if (deleteNote)
            {
                Destroy(other.gameObject);
                attack = false;
                hasNote = false;
                deleteNote = false;

                left = false;
                right = false;
                up = false;
                down = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = false;
        }
    }
}

