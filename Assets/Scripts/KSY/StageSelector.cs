using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public Button[] stageButtons;
    public string[] stageKeys = { "stage1", "stage2", "stage3", "stage4" };

    void Start()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            bool isUnlocked = ProgressManager.Instance.IsStageUnlocked(stageKeys[i]);
            stageButtons[i].interactable = isUnlocked;
        }
    }

    public void LoadStage(string stageName)
    {
        if (ProgressManager.Instance.IsStageUnlocked(stageName))
        {
            SceneManager.LoadScene(stageName);
        }
        else
        {
            Debug.Log("이 스테이지는 잠겨 있습니다!");
        }
    }
}
