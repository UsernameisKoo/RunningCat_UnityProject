using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// 두 번째 retry시 stage텅비는 버그 -> stage 활성화 하면서 플레이어랑 맵 좌표 초기화해야함
// 좌표 이동된 상태 그대로로 다시 시작해서 안 보이는 거임.
public class ClickButton : MonoBehaviour
{
    public GameManager gameManager;
    public void OnClickRetryButton()
    {
        gameManager.heart = 3;
        gameManager.Hearts[0].SetActive(true);
        gameManager.Hearts[1].SetActive(true);
        gameManager.Hearts[2].SetActive(true);
        gameManager.Me.SetActive(true);
        gameManager.Gameover.SetActive(false);
        gameManager.Stages[gameManager.stageIndex].SetActive(true);
    }
    public void OnClickHomeButton()
    {
        SceneManager.LoadScene(0);
    }

    
}
