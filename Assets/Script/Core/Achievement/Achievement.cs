using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    public bool IsUnlock { get; set; }

    private int CurrentPorgress;

    public void AddProgress(int amount)
    {
        CurrentPorgress += amount;
        AchievementManager.OnProgressUpdated?.Invoke(this);
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus()
    {
        if (CurrentPorgress >= ProgressToUnlock)
        {
            UnlockAchievement();
        }
    }

    private void UnlockAchievement()
    {
        IsUnlock = true;
        AchievementManager.OnAchievementUnlock?.Invoke(this);
    }

    public string GetProgress()
    {
        return $"{CurrentPorgress}/{ProgressToUnlock}";
    }

    public string GetProgressCompleted()
    {
        return $"{ProgressToUnlock}/{ProgressToUnlock}";
    }

    void OnEnable()
    {
        IsUnlock = false;
        CurrentPorgress = 0;
    }
}
