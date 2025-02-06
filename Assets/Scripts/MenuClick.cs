using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickHome()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickStage1()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentStageKey("stage_1");
        }
        else
        {
            Debug.LogError("GameManager가 씬에 존재하지 않습니다!");
        }

        SceneManager.LoadScene(1);
    }

    public void OnClickStage2()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentStageKey("stage_2");
        }
        else
        {
            Debug.LogError("GameManager가 씬에 존재하지 않습니다!");
        }
        SceneManager.LoadScene(2);
    }

    public void OnClickStage3()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentStageKey("stage_3");
        }
        else
        {
            Debug.LogError("GameManager가 씬에 존재하지 않습니다!");
        }
        SceneManager.LoadScene(3);
    }
}
