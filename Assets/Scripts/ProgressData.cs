// ProgressData.cs
using System;
using System.Collections.Generic;

[Serializable]
public class StageProgress
{
    public int total_potions;
    public int collected_potions;

    public float GetProgress()
    {
        return (float)collected_potions / total_potions * 100f;
    }
}

[Serializable]
public class ProgressData
{
    public Dictionary<string, StageProgress> stages;
    public Dictionary<string, bool> unlockedStages;
}
