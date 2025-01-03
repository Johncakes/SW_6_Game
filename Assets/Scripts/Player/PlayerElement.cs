using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public ColorEnum playerCurrentElement;
    public bool isClone;
    private CurrentColor CC;
    
    void Start()
    {
        CC = FindObjectOfType<CurrentColor>();
        if(CC!=null){
            playerCurrentElement=CC.color;
        }
        else
        {
            playerCurrentElement = ColorEnum.red;
            CC.color = playerCurrentElement;
        }

    }

    void Update()
    {
       if(CC!=null){
           
            playerCurrentElement=CC.color;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
          
            playerCurrentElement = ColorEnum.red;
            CC.color = playerCurrentElement;

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
           
            playerCurrentElement = ColorEnum.green;
            CC.color = playerCurrentElement;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            playerCurrentElement = ColorEnum.blue;
            CC.color = playerCurrentElement;
        }




    }

}
