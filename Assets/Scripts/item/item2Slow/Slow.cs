using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;


public class Slow : MonoBehaviour
{
     public float minX = -13f;
    public float maxX = 13f;
    public float minY = -5f;
    public float maxY = 5f;
    
    public Note NOTE;
    float originNoteSpawn;
    float originNoteSpeed;
    float newNoteSpawn;
    float newNoteSpeed;
    public void SlowNotes()
    {
        ObjectManager OM = FindObjectOfType<ObjectManager>();
        /*
        if(OM!=null){
            originNoteSpawn=OM.noteSpawnTime;
            newNoteSpawn=OM.noteSpawnTime*2;
            //originNoteSpeed=NOTE.speed;
            //newNoteSpeed=NOTE.speed/2; 
        }
        */
       
        if ( OM!= null)
        {
            Debug.Log("OM실행");
            //OM.noteSpawnTime =newNoteSpawn;  // speed 값을 반으로 줄임
            OM.spawnSlow=true;
            OM.isSlow=true;
        }
        else
        {
            Debug.LogWarning("OM스크립트가 할당되지 않았습니다.");
        }
        if ( NOTE== null)
        {
            //NOTE.speed = newNoteSpeed;  // speed 값을 반으로 줄임
        }
        else
        {
            Debug.LogWarning("스크립트가 할당되지 않았습니다.");
        }
        
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        foreach (GameObject note in notes)
        {
            Vector3 pos = note.transform.position;

            // x와 y 범위 안에 있는 오브젝트만 느리게 만들기
            if (pos.x >= minX && pos.x <= maxX && pos.y >= minY && pos.y <= maxY)
            {
                  // Note 컴포넌트를 가져옴
                Note noteComponent = note.GetComponent<Note>();

                if (noteComponent != null)
                {
                    noteComponent.speed = 2.5f;  // speed 값을 반으로 줄임
                }
                else
                {
                    Debug.LogWarning("Note 컴포넌트가 없는 오브젝트 ");
                }
                
                
            }
             
            
               
        }
        
        
       
       
            
    }
       
    
    
    
     
}
