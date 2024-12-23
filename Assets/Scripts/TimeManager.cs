using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    private bool scoreManagerActive = false;
    public Sprite[] numbers; //숫자 스프라이트
    public GameObject[] Timer;

    private float startTime;  // 게임 시작 시간
    public float timeLimit;  // 제한시간
    public float timeRemaining; // 남은 시간
    public bool isGameOver;
    public bool isGameClear;
    bool hasTriggered; // 시간 업데이트 한 번만 하기
    void Start()
    {
        // 스코어 저장하는 용도
        isGameOver = false;
        hasTriggered = false;
        isGameClear = false;

        startTime = Time.time;  // 게임 시작 시간을 현재 시간으로 설정

        if (GameObject.Find("ScoreManager") != null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            scoreManagerActive = true;
        }

        int n = (int)timeLimit;

        for (int i = 2; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Timer[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        float timeElapsed = Time.time - startTime;  // 경과 시간 계산
        timeRemaining = timeLimit - timeElapsed;  // 남은 시간 계산
        int n = (int)timeRemaining;
        for (int i = 2; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Timer[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
        //if(timeRemaining <= 0) 일시 게임오버
    }

    public void UpdateCurScore()
    {
        if (!hasTriggered && scoreManagerActive)
        {
            hasTriggered = true;
            scoreManager.addScore((int)timeRemaining);
        }
    }
}
