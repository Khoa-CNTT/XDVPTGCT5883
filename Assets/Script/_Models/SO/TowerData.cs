using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    public GameObject towerPrefab;
    public string towerName;
    public float damage;
    public float attackSpeed;
    public float range;
}
