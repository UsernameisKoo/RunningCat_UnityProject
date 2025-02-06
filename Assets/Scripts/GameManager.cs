using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    public int score = 0;
    public TMP_Text scoreText;
    private string currentStageKey = "stage_1"; // 기본값



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 변경 시 자동으로 호출
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 로드됨: " + scene.name);
        FindScoreText(); // 씬이 로드될 때 scoreText 다시 찾기
        ResetScore();    // 스테이지 시작 시 스코어 초기화
    }

    public string GetCurrentStageKey()
    {
        return currentStageKey;
    }

    public void SetCurrentStageKey(string newStageKey)
    {
        currentStageKey = newStageKey;
        Debug.Log($"현재 스테이지 변경: {currentStageKey}");
    }

    void FindScoreText()
    {
        GameObject textObject = GameObject.Find("scoreText/scoreText");
        if (textObject != null)
        {
            Debug.Log("textObject Name: " + textObject.name);
            scoreText = textObject.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("ScoreText를 찾을 수 없습니다! 씬에 scoreText 오브젝트가 있는지 확인하세요.");
        }
    }

    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        Debug.Log("스테이지 재시작 - Score 초기화 완료");
    }


    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        Debug.Log("Score: " + score);
    }

    public void HeartDown()
    {
        if (heart > 0 && Me.layer == 10)
        {
            heart--;
            Hearts[heart].SetActive(false);

            if (heart == 0)
            {
                Invoke("GameOver", 0.5f);
            }
        }
    }

  

    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false);
        Gameover.SetActive(true);
        
    }


    public void StageClear()
    {
        Gameclear.SetActive(true);
    }
}
