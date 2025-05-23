using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCard : MonoBehaviour
{
    [SerializeField] private Image achievementImage;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Button rewardButton;

    public Achievement AchievementLoaded { get; set; }

    public void SetupAchievement(Achievement achievement)
    {
        AchievementLoaded = achievement;
        achievementImage.sprite = achievement.Sprite;
        Title.text = achievement.Title;
        progress.text = achievement.GetProgress();
        reward.text = achievement.GoldReward.ToString();
    }

    public void GetReward()
    {
        if (AchievementLoaded.IsUnlock)
        {
            CurrentcySystem.Instance.AddCoins(AchievementLoaded.GoldReward);
            rewardButton.gameObject.SetActive(false);
        }
    }

    private void LoadAchievementProgress()
    {
        if (AchievementLoaded.IsUnlock)
        {
            progress.text = AchievementLoaded.GetProgressCompleted();
    }
        else
        {
            progress.text = AchievementLoaded.GetProgress();
        }
    }

    private void CheckRewardButtonStatus()
    {
        if (AchievementLoaded.IsUnlock)
        {
            rewardButton.interactable = true;
        }
        else
        {
            rewardButton.interactable = false;
        }
    }

    private void UpdateProgress(Achievement achievementWithProgress)
    {
        if (AchievementLoaded == achievementWithProgress)
        {
            LoadAchievementProgress();
        }
    }

    private void AchievementUnlocked(Achievement achievement)
    {
        if (AchievementLoaded == achievement)
        {
            CheckRewardButtonStatus();
        }
    }

    void OnEnable()
    {
        CheckRewardButtonStatus();
        LoadAchievementProgress();
        AchievementManager.OnProgressUpdated += UpdateProgress;
        AchievementManager.OnAchievementUnlock += AchievementUnlocked;
    }

    void OnDisable()
    {
        AchievementManager.OnProgressUpdated -= UpdateProgress;
        AchievementManager.OnAchievementUnlock -= AchievementUnlocked;
    }
}
