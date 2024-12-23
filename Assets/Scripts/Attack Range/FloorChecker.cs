using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    public bool hasPlayer = false;
    public int FloorLevel;

    private AttackNodeInRange parent;

    void Start()
    {
        AttackNodeInRange parent = GetComponentInParent<AttackNodeInRange>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")//||other.gameObject.tag == "clone"
        {
            AttackNodeInRange parent = GetComponentInParent<AttackNodeInRange>();
            hasPlayer = true;
            parent.OnChildTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")//||other.gameObject.tag == "clone"
        {
            AttackNodeInRange parent = GetComponentInParent<AttackNodeInRange>();
            hasPlayer = false;
            if (parent != null)
            {
                parent.OnChildTrigger();
            }

        }
    }



}
