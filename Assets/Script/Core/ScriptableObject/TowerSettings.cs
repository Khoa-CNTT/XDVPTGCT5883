using System.Collections.Generic;
using dang;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretShopSettings")]
public class TowerSettings : ScriptableObject
{
    [SerializeField] public GameObject towerPrefabs;
    [SerializeField] private List<TowerData> towerSettings;

    public List<TowerData> GetTowerData() => towerSettings;
}
