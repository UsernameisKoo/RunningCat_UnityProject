using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글턴 인스턴스

    public Transform finishTransform;
    public GameObject stage1;
    public int stageIndex;
    public GameObject Me;
    public GameObject[] Stages;
    public GameObject Home;
    public GameObject Gameover;
    public GameObject Gameclear;
    public GameObject[] Hearts;
    public int heart;
    public int score = 0;  // 점수 변수
    public TMP_Text scoreText; // UI 점수 텍스트

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void AddScore(int amount)
    {
        score += amount; 
        scoreText.text = "Score: " + score;  
        Debug.Log("Score: " + score); 
    }

    public void HeartDown()
    {
        if (heart > 0 && Me.layer == 10)
        {
            heart--;
            Hearts[heart].SetActive(false);

            // 죽음 -> 홈으로 돌아감
            if (heart == 0)
            {
                Invoke("GameOver", 0.5f);
            }
        }
    }

    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false); // 현재 스테이지 비활성화
        Gameover.SetActive(true); // 게임오버를 실행 시킴
    }

    public void StageClear()
    {
        Gameclear.SetActive(true);
    }
}
