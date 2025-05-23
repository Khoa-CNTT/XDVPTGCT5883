using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    public static Action<TowerData> OnplaceTower;
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerCost;
    private TowerData currentTowerData;

    public void SetupTurretButton(TowerSettings towerSettings, int index)
    {
        var towers = towerSettings.GetTowerData();
        if (index < 0 || index >= towers.Count) return;

        TowerData data = towers[index];
        currentTowerData = data;

        towerImage.sprite = data.towerSprite;
        towerCost.text = data.TowerShopCost.ToString();
    }

    public void PlaceTurret()
    {
        if (CurrentcySystem.Instance.TotalCoins >= currentTowerData.TowerShopCost)
        {
            CurrentcySystem.Instance.RemoveCoins((int)currentTowerData.TowerShopCost);
            OnplaceTower?.Invoke(currentTowerData);
        }
    }
}
