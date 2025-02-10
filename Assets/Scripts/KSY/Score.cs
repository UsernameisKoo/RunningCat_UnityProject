using UnityEngine;

public class Score : MonoBehaviour
{
    public enum PotionType { Normal, Big, Rainbow } // 포션 종류
    public PotionType potionType = PotionType.Normal;

    public int normalPotionValue = 10;
    public int bigPotionValue = 20;
    public int rainbowPotionValue = 50;

    public int normalPotionProgress = 1;
    public int bigPotionProgress = 2;
    public int rainbowPotionProgress = 5;

    private ProgressManager progressManager;

    void Start()
    {
        progressManager = FindAnyObjectByType<ProgressManager>();

        if (progressManager == null)
        {
            Debug.LogError("ProgressManager가 씬에서 찾을 수 없습니다.");
            return;
        }

        string currentStageKey = GameManager.Instance.GetCurrentStageKey();

        if (progressManager.progressData.stages.ContainsKey(currentStageKey))
        {
            progressManager.progressData.stages[currentStageKey].newCollected = 0;
            progressManager.SaveProgress();
            Debug.Log($"[{currentStageKey}] newCollected를 0으로 초기화");
        }
        else
        {
            Debug.LogWarning($"[{currentStageKey}] 스테이지 데이터가 없어서 초기화할 수 없습니다.");
        }
    }

 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && progressManager != null)
        {
            // 현재 진행 중인 스테이지 가져오기 (GameManager에서 현재 스테이지 가져오도록 수정)
            string currentStageKey = GameManager.Instance.GetCurrentStageKey();

            if (!progressManager.progressData.stages.ContainsKey(currentStageKey))
            {
                Debug.LogWarning($"'{currentStageKey}' 스테이지 데이터가 없어서 초기화합니다.");
                progressManager.progressData.stages[currentStageKey] = new StageProgress { total_potions = 8, collected_potions = 0, newCollected = 0, };
                progressManager.SaveProgress();
            }

            StageProgress stage = progressManager.progressData.stages[currentStageKey];

            int potionProgress = potionType switch
            {
                PotionType.Normal => normalPotionProgress,
                PotionType.Big => bigPotionProgress,
                PotionType.Rainbow => rainbowPotionProgress,
                _ => 0
            };


            stage.newCollected = Mathf.Min(stage.newCollected + potionProgress, stage.total_potions);

            Debug.Log($"[{currentStageKey}] 현재 포션 진행도: {stage.collected_potions}, 추가할 값: {potionProgress}, 결과 값: {stage.newCollected}");

            if (stage.newCollected > stage.collected_potions)
            {
                stage.collected_potions = stage.newCollected;
                progressManager.progressData.stages[currentStageKey] = stage;
                progressManager.SaveProgress();

                Debug.Log($"[{currentStageKey}] 포션 진행도 업데이트 완료! 현재 진행도: {stage.collected_potions}/{stage.total_potions}");
            }

            // 점수 추가
            int scoreValue = potionType switch
            {
                PotionType.Normal => normalPotionValue,
                PotionType.Big => bigPotionValue,
                PotionType.Rainbow => rainbowPotionValue,
                _ => 0
            };

            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject); // 먹은 포션 제거
        }
    }
}
