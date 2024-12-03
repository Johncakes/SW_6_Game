using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAnimations : MonoBehaviour
{

    public float attackDelay = 0.3f;

    PlayerMovement playerMovement;
    PlayerElement playerElement;

    Animator animator;
    PlayerAnimationEnum currentState;

    public GameObject targetObject;

    public bool isAttacking = false;
    public bool pressedAttack = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerElement = GetComponent<PlayerElement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pressedAttack = true;
            ChangeAnimationState(GetAttackAnimation("up"));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pressedAttack = true;
            ChangeAnimationState(GetAttackAnimation("down"));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pressedAttack = true;
            ChangeAnimationState(GetAttackAnimation("side"));
        }
        
        Vector3 currentPosition = transform.position;
        if (currentPosition.y>-3 && !pressedAttack)//|| !playerMovement.doRunning) //Change to idle when above floor 1
        {
            ChangeAnimationState(GetIdleAnimation());
        }
        else if (pressedAttack)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("AttackComplete", attackDelay);
            }
        }
        else
        {            
            ChangeAnimationState(GetRunAnimation());

        }
    }

    PlayerAnimationEnum GetAttackAnimation(string direction)
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                if (direction == "up") return PlayerAnimationEnum.blue_attack_up;
                if (direction == "down") return PlayerAnimationEnum.blue_attack_down;
                return PlayerAnimationEnum.blue_attack_side;
            case ColorEnum.green:
                if (direction == "up") return PlayerAnimationEnum.green_attack_up;
                if (direction == "down") return PlayerAnimationEnum.green_attack_down;
                return PlayerAnimationEnum.green_attack_side;
            default:
                if (direction == "up") return PlayerAnimationEnum.red_attack_up;
                if (direction == "down") return PlayerAnimationEnum.red_attack_down;
                return PlayerAnimationEnum.red_attack_side;
        }
    }

    PlayerAnimationEnum GetIdleAnimation()
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                return PlayerAnimationEnum.blue_idle;
            case ColorEnum.green:
                return PlayerAnimationEnum.green_idle;
            default:
                return PlayerAnimationEnum.red_idle;
        }
    }
    PlayerAnimationEnum GetRunAnimation()
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                return PlayerAnimationEnum.blue_run;
            case ColorEnum.green:
                return PlayerAnimationEnum.green_run;
            default:
                return PlayerAnimationEnum.red_run;
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
        pressedAttack = false;
    }

    void ChangeAnimationState(PlayerAnimationEnum newState)
    {
        if (currentState == newState) return;

        // Debug.Log("Changing state to: " + newState.ToString());
        animator.Play(newState.ToString());

        currentState = newState;

    }
}