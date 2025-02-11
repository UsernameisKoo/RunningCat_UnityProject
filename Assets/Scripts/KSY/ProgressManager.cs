using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    private string filePath;
    public ProgressData progressData = new ProgressData();

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
            return;
        }

        filePath = Path.Combine(Application.persistentDataPath, "progress.json");
        Debug.LogWarning("progressData.stages 초기화합니다.");
        InitializeOrLoadProgress();

        //  강제로 stages 초기화 확인 
        if (progressData.stages == null)
        {
            Debug.LogWarning("progressData.stages가 null이므로 기본 값으로 초기화합니다.");
            InitializeProgress();
        }
    }

    void InitializeOrLoadProgress()
    {
        if (File.Exists(filePath))
        {
            LoadProgress();
        }
        else
        {
            Debug.Log("초기화");
            InitializeProgress();
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        if (progressData == null)
        {
            Debug.LogError("progressData가 null이므로 저장할 수 없음!");
            return;
        }

        string json = JsonUtility.ToJson(progressData, true);

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Json 변환 실패! progressData가 비어있거나 직렬화 오류 발생");
            return;
        }

        File.WriteAllText(filePath, json);
        Debug.Log($"progress.json 저장 완료! 파일 경로: {filePath}\n{json}");
    }


    public void LoadProgress()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("저장된 progress.json 파일이 없음, 기본값으로 초기화합니다.");
            InitializeProgress();
            return;
        }

        string json = File.ReadAllText(filePath);
        progressData = JsonUtility.FromJson<ProgressData>(json);

        if (progressData.stages == null)
        {
            Debug.LogWarning("로드된 데이터에서 stages가 null입니다. 기본 값으로 초기화합니다.");
            InitializeProgress();
        }

        if (progressData == null || progressData.unlockedStages == null)
        {
            Debug.LogWarning("로드된 데이터가 null이므로 초기화합니다.");
            InitializeProgress();
        }

    }

    public bool AllPotionsCollected()
    {
        foreach (var stage in progressData.stages)
        {
            if (stage.Key != "stage_4" && stage.Value.collected_potions < stage.Value.total_potions)
            {
                return false; // 하나라도 덜 모은 스테이지가 있다면 false 반환
            }
        }

        // 모든 포션을 모았으면 stage4(Ending) 해금
        Debug.LogWarning("엔딩 스테이지 해금!");
        UnlockStage("stage4");
        SaveProgress(); // 변경사항 저장
        return true;
    }




    void InitializeProgress()
    {
        if (progressData.stages == null)
        {
            progressData.stages = new Dictionary<string, StageProgress>
            {
                { "stage_1", new StageProgress { total_potions = 8, collected_potions = 0 } },
                { "stage_2", new StageProgress { total_potions = 8, collected_potions = 0 } },
                { "stage_3", new StageProgress { total_potions = 8, collected_potions = 0 } }
            };
        }


        if (progressData.unlockedStages == null)
        {
            progressData.unlockedStages = new Dictionary<string, bool>();
        }
        if (!progressData.unlockedStages.ContainsKey("stage1")) progressData.unlockedStages["stage1"] = true;
        if (!progressData.unlockedStages.ContainsKey("stage2")) progressData.unlockedStages["stage2"] = false;
        if (!progressData.unlockedStages.ContainsKey("stage3")) progressData.unlockedStages["stage3"] = false;
        if (!progressData.unlockedStages.ContainsKey("stage4")) progressData.unlockedStages["stage4"] = false;

        Debug.LogWarning("스테이지 초기화 완료");
        Debug.Log($"Progress 파일 경로: {Application.persistentDataPath}");

        SaveProgress();  // 수정된 초기화 내용을 저장
    }

    public void UnlockStage(string stageKey)
    {
        if (!progressData.unlockedStages.ContainsKey(stageKey))
        {
            string alternativeKey = stageKey.Replace("_", ""); // "stage_1" → "stage1"
            if (progressData.unlockedStages.ContainsKey(alternativeKey))
            {
                stageKey = alternativeKey;
            }
            else
            {
                Debug.LogWarning($"UnlockStage: {stageKey}가 존재하지 않음");
                return;
            }
        }

        Debug.Log($"스테이지 잠금 해제");
        progressData.unlockedStages[stageKey] = true;
        SaveProgress(); // 변경 사항을 저장하여 유지
    }


    public bool IsStageUnlocked(string stageKey)
    {
        // 키 값 통일 (예: "stage_1"이 저장된 데이터에 없으면 "stage1" 체크)
        if (!progressData.unlockedStages.ContainsKey(stageKey))
        {
            string alternativeKey = stageKey.Replace("_", ""); // "stage_1" → "stage1"
            if (progressData.unlockedStages.ContainsKey(alternativeKey))
            {
                return progressData.unlockedStages[alternativeKey];
            }
            return false;
        }
        return progressData.unlockedStages[stageKey];
    }



}
