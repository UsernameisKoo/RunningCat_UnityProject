using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하는 경우
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    public string stageKey; // 스테이지 키 (예: "stage_1")
    public ProgressManager progressManager;
    public UnityEngine.UI.Slider progressBar;
    public TextMeshProUGUI progressText; // TextMeshPro UI 사용 시
    // public Text progressText; // 일반 UnityEngine.UI.Text 사용 시

    void Start()
    {
        Debug.Log("ProgressBar Start() 호출됨");

        progressManager = FindFirstObjectByType<ProgressManager>();

        if (progressManager == null)
        {
            Debug.LogError("ProgressManager를 찾을 수 없습니다.");
            return;
        }

        if (progressManager.progressData == null)
        {
            Debug.LogError("progressData가 초기화되지 않았습니다.");
            return;
        }

        if (progressManager.progressData.stages == null)
        {
            Debug.LogError("stages Dictionary가 초기화되지 않았습니다.");
            return;
        }

        if (progressBar == null)
        {
            Debug.LogError("ProgressBar가 할당되지 않았습니다.");
            return;
        }

        if (progressText == null)
        {
            Debug.LogError("ProgressText가 할당되지 않았습니다.");
            return;
        }

        Debug.Log("Start() 실행 완료");
    }



    void Update()
    {
        if (string.IsNullOrEmpty(stageKey))
        {
            Debug.LogError("stageKey가 설정되지 않았습니다.");
            return;
        }

        if (progressManager.progressData.stages.ContainsKey(stageKey))
        {
            StageProgress stage = progressManager.progressData.stages[stageKey];

            float progress = (float)stage.collected_potions / (float)stage.total_potions;
            progressBar.value = progress;
            progressText.text = $"{stage.collected_potions} / {stage.total_potions}";
        }
        else
        {
            Debug.LogWarning($"스테이지 키 '{stageKey}' 가 stages에 존재하지 않습니다.");
        }
    }
}
