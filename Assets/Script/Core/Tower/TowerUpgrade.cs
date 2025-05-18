using dang;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    public int UpgradeCost { get; set; }

    private TowerProjectile _towerProjectile;

    void Start()
    {
        _towerProjectile = GetComponent<TowerProjectile>();
        UpgradeCost = upgradeInitialCost;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTower();
        }
    }

    private void UpgradeTower()
    {
        if (CurrentcySystem.Instance.TotalCoins >= UpgradeCost)
        {
            _towerProjectile.Damage += damageIncremental;
            _towerProjectile.DelayPershot -= delayReduce;
            UpdateUpgrade();
        }
    }

    private void UpdateUpgrade()
    {
        CurrentcySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
    }
}
