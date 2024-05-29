using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public int totalPoint;
    //public int stagePoint;
    public int stageIndex;
    public PlayerMove player;
    public GameObject Me;
    public GameObject[] Stages;
    public GameObject Home;
    public GameObject Gameover;
    public GameObject Gameclear;
    public GameObject[] Hearts;
    public int heart;

    public void NextStage()
    {
        // 두 번째 retry시 stage텅비는 버그 -> stage 활성화 하면서 플레이어랑 맵 좌표 초기화해야함
        // 좌표 이동된 상태 그대로로 다시 시작해서 안 보이는 거임.
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;

            Invoke("StageClear", 2);

            //Stages[stageIndex].SetActive(true);
            //PlayerReposition();
        }
        else //Game Clear
        {
            Invoke("StageClear", 2);
        }


    }

    public void HeartDown()
    {
        if (heart > 0)
        {
            heart--;
            Hearts[2 - heart].SetActive(false);

            // 죽음 -> 홈으로 돌아감
            if (heart == 0)
            {
                Invoke("GameOver", 2);
                //Home.SetActive(true);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //낭떠러지 떨어지면
        if (collision.gameObject.tag == "Player")
        {
            HeartDown();
            HeartDown();
            HeartDown();
        }
    }
    void GameOver()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false);
        Gameover.SetActive(true);
    }

    void StageClear()
    {
        Me.SetActive(false);
        Stages[stageIndex].SetActive(false);
        Gameclear.SetActive(true);
    }

}
