using dang;
using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerUIManager : Singleton<TowerUIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject towerShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject RangeUI;
    [SerializeField] private GameObject SettingsScreen;
    [SerializeField] private GameObject AchievementPanel;
    [SerializeField] private GameObject GameOverPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI towerLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinText;
    [SerializeField] private TextMeshProUGUI totalLifeText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private Node _currentNodeSelected;

    void Update()
    {
        totalCoinText.text = CurrentcySystem.Instance.TotalCoins.ToString();
        totalLifeText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"{LevelManager.Instance.CurrentWave.ToString()}/{LevelManager.Instance.MaxWave.ToString()}";
    }

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            towerShopPanel.SetActive(true);
            if (towerShopPanel.activeInHierarchy)
            {
                closeNodeUI();
            }
        }
        else
        {
            showNodeUI();
            if (nodeUIPanel.activeInHierarchy)
            {
                towerShopPanel.SetActive(false);
            }
        }
    }

    public void UpgradeTurret()
    {
        _currentNodeSelected.TowerController.TowerUpgrade.UpgradeTower();
        UpdateUpgradeText();
        UpdateLevelText();
        UpdateSellText();
    }

    public void SellTower()
    {
        _currentNodeSelected.SellTower();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        if (LevelManager.Instance.IsFinalWave)
        {
            gameOverText.text = "Victory!";
            int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLevel > currentUnlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
                PlayerPrefs.Save();
            }
        }
        else
        {
            gameOverText.text = "You Lose!";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void showNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateLevelText();
        UpdateSellText();
    }

    public void closeNodeUI()
    {
        _currentNodeSelected.CloseAttackRangeSprite();
        nodeUIPanel.SetActive(false);
    }

    public void OpenSettignsScreen()
    {
        Time.timeScale = 0f;
        SettingsScreen.SetActive(true);
    }

    public void CloseSettingsScreen()
    {
        Time.timeScale = 1f;
        SettingsScreen.SetActive(false);
    }

    public void OpenAchievement()
    {
        AchievementPanel.SetActive(true);
    }

    public void CloseAchievement()
    {
        AchievementPanel.SetActive(false);
    }


    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("UIMainMenuScrene");
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.TowerController.TowerUpgrade.UpgradeCost.ToString();
    }

    private void UpdateLevelText()
    {
        towerLevelText.text = $"Level {_currentNodeSelected.TowerController.TowerUpgrade.Level}";
    }

    private void UpdateSellText()
    {
        int sellAmount = _currentNodeSelected.TowerController.TowerUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }

    void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
