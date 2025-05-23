using System;
using dang;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{

    private TowerController towerController;
    private TowerProjectile _towerProjectile;

    [Header("Upgrade")]
    // [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    // [SerializeField] private float delayReduce;

    [Header("Sell")]
    [Range(0, 1)]
    [SerializeField] private float sellPert;
    public float SellPercent { get; set; }

    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    void Start()
    {
        _towerProjectile = GetComponent<TowerProjectile>();
        towerController = GetComponent<TowerController>();

        UpgradeCost = (int)towerController.towerStats.TowerUpgradeShopCost;
        SellPercent = sellPert;
        Level = 1;
    }

    public void UpgradeTower()
    {
        if (CurrentcySystem.Instance.TotalCoins >= UpgradeCost)
        {
            _towerProjectile.Damage += damageIncremental;
            // _towerProjectile.AttackSpeed -= delayReduce;
            UpdateUpgrade();
        }
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * sellPert);
        return sellValue;
    }

    private void UpdateUpgrade()
    {
        CurrentcySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
}
