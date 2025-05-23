using System.Collections;
using dang;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPostition;
    private float delayBtwAttacks;
    private float _NextAttackTime;

    public float Damage { get; set; }
    public float AttackSpeed { get; set; }

    private ObjectPooling _pool;
    private TowerController _tower;
    private Bullet _currentBulletLoaded;
    private TowerInit _towerData;
    private bool _isAttacking = false;

    void Awake()
    {
        _pool = GetComponent<ObjectPooling>();
        _tower = GetComponent<TowerController>();
        _towerData = GetComponent<TowerInit>();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => _towerData.towerDamage != 0 && _towerData.towerAttackSpeed != 0);
        Damage = _towerData.towerDamage;
        AttackSpeed = _towerData.towerAttackSpeed;

        delayBtwAttacks = 1f / AttackSpeed;
    }

    void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }
    }

    private void LoadProjectile()
    {
        GameObject bulletInstance = _pool.GetInstanceFromPool();

        bulletInstance.transform.localPosition = bulletSpawnPostition.position;
        bulletInstance.transform.SetParent(bulletSpawnPostition);

        _currentBulletLoaded = bulletInstance.GetComponent<Bullet>();
        _currentBulletLoaded.TowerOwner = this;
        _currentBulletLoaded.ResetProjectile();
        _currentBulletLoaded.Damage = Damage;
        bulletInstance.SetActive(true);
    }

    private bool IsTurretEmpty()
    {
        return _currentBulletLoaded == null;
    }

    public void ResetTowerProjectile()
    {
        _currentBulletLoaded = null;
    }

    public void Shoot()
    {
        if (_tower == null || _tower.CurrentEnemyTarget == null || IsTurretEmpty()) return;
        if (_tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth <= 0f) return;

        if (Time.time < _NextAttackTime)
        {
            return;
        }

        _currentBulletLoaded.transform.parent = null;
        _currentBulletLoaded.SetEnemy(_tower.CurrentEnemyTarget);
        _isAttacking = true;

        _NextAttackTime = Time.time + delayBtwAttacks;
    }

}
