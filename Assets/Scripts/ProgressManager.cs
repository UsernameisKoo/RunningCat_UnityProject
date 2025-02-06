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
            InitializeProgress();
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(progressData, true);
        File.WriteAllText(filePath, json);
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

    void InitializeProgress()
    {
        if (progressData.stages == null)
        {
            progressData.stages = new Dictionary<string, StageProgress>
            {
                { "stage_1", new StageProgress { total_potions = 8, collected_potions = 0 } },
                { "stage_2", new StageProgress { total_potions = 30, collected_potions = 0 } },
                { "stage_3", new StageProgress { total_potions = 50, collected_potions = 0 } }
            };
        }


        if (progressData.unlockedStages == null)
        {
            progressData.unlockedStages = new Dictionary<string, bool>();
        }
        {
            progressData.unlockedStages["stage1"] = true;
        progressData.unlockedStages["stage2"] = false;
        progressData.unlockedStages["stage3"] = false;
        };
    }

    public bool IsStageUnlocked(string stageKey)
    {
        return progressData.unlockedStages.ContainsKey(stageKey) && progressData.unlockedStages[stageKey];
    }

    public void UnlockStage(string stageKey)
    {
        if (progressData.unlockedStages.ContainsKey(stageKey))
        {
            progressData.unlockedStages[stageKey] = true;
            SaveProgress();
        }
    }
}
