using dang;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int live = 10;
    [SerializeField] private int maxWave = 5;

    private bool isFinalWave = false;

    public int MaxWave { get; set; }
    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    public bool IsFinalWave { get; set; }

    void OnEnable()
    {
        EnemiesController.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    void OnDisable()
    {
        EnemiesController.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }

    void Start()
    {
        TotalLives = live;
        MaxWave = maxWave;
        CurrentWave = 1;
        isFinalWave = false;
    }

    private void WaveCompleted()
    {
        if (CurrentWave == maxWave)
        {
            isFinalWave = true;
            IsFinalWave = isFinalWave;
            TowerUIManager.Instance.ShowGameOverPanel();
        }
        else
        {
            CurrentWave++;
        }

        AchievementManager.Instance.AddProgress("Waves_Easy_01", 1);
        AchievementManager.Instance.AddProgress("Waves_Medium_01", 1);
        AchievementManager.Instance.AddProgress("Waves_Hard_01", 1);
    }

    private void ReduceLives(EnemiesController enemy)
    {
        TotalLives--;

        if (TotalLives <= 0)
        {
            TotalLives = 0;
            isFinalWave = true;
            IsFinalWave = isFinalWave;
            TowerUIManager.Instance.ShowGameOverPanel();
        }
    }
}
