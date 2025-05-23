using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    public Sprite towerSprite;
    public GameObject archerPrefab;
    public string towerName;
    public float damage;
    public float attackSpeed;
    public float range;
    public float TowerShopCost;
    public float TowerUpgradeShopCost;
}
