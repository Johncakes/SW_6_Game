using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectManager : MonoBehaviour
{
    public GameObject notePrefab;
    private NotePatterns notePatterns;
    public float noteSpawnTime;
    private float originalNoteSpawnTime;
    public float noteSpawnTimeMin;
    public float noteSpawnTimeMax;
    GameObject[] note;
    public int cnt;
    public float levelNoteSpeed;
    public bool isSlow;
    public bool changingNoteLocation;
    public bool oppositeNoteArrow;
    public bool notActedNote;
    public bool fadeNote;
    public bool unifyNote;


    [Range(0, 2)]
    public int floors;
    //boss appear
    public bool isEnd;
    void Awake()
    {
        notePatterns = GetComponent<NotePatterns>();
        originalNoteSpawnTime = noteSpawnTime;

        note = new GameObject[500];
        Generate();

        Invoke("makeObj", 1f);
        // Boss pattern
    }

    void Generate()
    {
        for (int i = 0; i < note.Length; i++)
        {
            note[i] = Instantiate(notePrefab);
            note[i].SetActive(false);
        }
    }

    public void GameEnd()
    {
        isEnd = true;
        for (int i = 0; i < note.Length; i++)
        {
            note[i].SetActive(false);
        }
    }

    void makeObj()
    {
        if (isEnd)
        {
            Debug.Log("생성 금지");
            return;
        }

        if (notePatterns.patnum >= 0) // Boss Pattern
        {
            notePatterns.activatePattern();
        }
        else // Normal Pattern
        {

            SetNoteLine(true, 0);
            SetNoteColor(true, 0);
            SetNoteDirection(true, 0);
            setNoteSpeed(false, 0);
            setNoteAttribute();
            SetNotetoActive();

        }

        if (isSlow)
        {
            noteSpawnTime = originalNoteSpawnTime * 2;
        }
        else
        {
            noteSpawnTime = originalNoteSpawnTime;
        }

        Invoke("makeObj", noteSpawnTime);
    }

    public void SetNoteLine(bool isRandom, int line)
    {
        if (isRandom)
        {
            note[cnt].GetComponent<Note>().spawnLine = Random.Range(0, floors + 1);
        }
        else
        {
            note[cnt].GetComponent<Note>().spawnLine = line;
        }
    }

    public void SetNoteColor(bool isRandom, int setColor)
    {
        if (isRandom)
        {
            note[cnt].GetComponent<Note>().coloridx = Random.Range(0, 3);
        }
        else
        {
            note[cnt].GetComponent<Note>().coloridx = setColor;
        }

        notePatterns.patternNoteColoridx = note[cnt].GetComponent<Note>().coloridx;
    }

    public void setNoteSpeed(bool isCustom, float speed)
    {
        if (isCustom)
        {
            note[cnt].GetComponent<Note>().speed = speed;
        }
        else
        {
            note[cnt].GetComponent<Note>().speed = levelNoteSpeed;
        }
    }

    public void SetNoteDirection(bool isRandom, int setDirection)
    {
        if (isRandom)
        {
            note[cnt].GetComponent<Note>().arrowidx = Random.Range(0, 4);
        }
        else
        {
            note[cnt].GetComponent<Note>().arrowidx = setDirection;
        }

        notePatterns.patternNoteArrowidx = note[cnt].GetComponent<Note>().arrowidx;
    }

    public void setNoteAttribute()
    {

        if (isSlow)
            note[cnt].GetComponent<Note>().speed *= 0.5f;
        if (oppositeNoteArrow)
            note[cnt].GetComponent<Note>().isOpposite = true;
        if (notActedNote)
            note[cnt].GetComponent<Note>().isNotacted = true;
        if (fadeNote)
            note[cnt].GetComponent<Note>().isFaded = true;
        if (unifyNote)
        {
            GameObject[] activeNote = GameObject.FindGameObjectsWithTag("Note");

            foreach (GameObject n in activeNote)
            {
                n.GetComponent<Note>().isSame = true;
            }
        }
    }

    public void SetNotetoActive()
    {
        note[cnt++].SetActive(true);
    }
}

