using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public string currentStageKey; // 현재 스테이지 (예: "stage1")
    public string nextStageKey; // 다음 스테이지 (예: "stage2")

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{currentStageKey} 클리어! {nextStageKey} 스테이지 잠금 해제");

            // ProgressManager를 통해 다음 스테이지 잠금 해제
            ProgressManager.Instance.UnlockStage(nextStageKey);
        }
    }
}
