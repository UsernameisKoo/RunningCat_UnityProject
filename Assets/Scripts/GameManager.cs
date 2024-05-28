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
        //Change Stage
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
