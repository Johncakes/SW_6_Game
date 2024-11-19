using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackNodeInRange : MonoBehaviour
{
    public bool hasNote = false;
    public bool attack = false;
    private FloorChecker floorChecker;
    public bool playerInRange = false;

    public HealthBarController healthBarController;
    private GameObject player;
    private PlayerElement playerElement;
    public ArrowDirectionEnum arrowDirection;

    void Start()
    {
        floorChecker = GetComponentInChildren<FloorChecker>();
        player = GameObject.Find("Player");
        playerElement = player.GetComponent<PlayerElement>();
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

        if (hasNote && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.right;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.down;
            }
        }
        else if (!playerInRange)
        {
            attack = false;
        }
    }

    void ShakeAttackBar()
    {
        AttackRangeShaker attackRangeShaker = GetComponentInParent<AttackRangeShaker>();
        attackRangeShaker.StartShaking();

    }

    void OnTriggerEnter2D(Collider2D other) // 공격범위에 들어왔을때
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
                if (note.noteArrowDirection == arrowDirection && playerElement.playerCurrentElement == note.noteColor)
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
                note.StartMovingToBoss();
                attack = false;
                hasNote = false;
                deleteNote = false;

                left = false;
                right = false;
                up = false;
                down = false;
                /*
                HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
                if(healthBarController!=null)
                {
                    if(!note.isNotacted)
                        HealthBarController.Instance.Heal();
                    else
                    {
                        Debug.LogError("HealthBarController not found on Player object.");
                    }
                }
                else
                {
                    ShakeAttackBar();
                    Destroy(other.gameObject);
                    attack = false;
                    hasNote = false;
                    arrowDirection = ArrowDirectionEnum.none;

                    HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
                    if (healthBarController != null)
                    {
                        HealthBarController.Instance.TakeDamage();
                    }
                    else
                    {
                        Debug.LogError("HealthBarController not found on Player object.");
                    }
                }
                */
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = false;
            Note note = other.gameObject.GetComponent<Note>();
            if(note!=null)
            {
                note.StartMovingToPlayer();
            }
        }
    }
}

