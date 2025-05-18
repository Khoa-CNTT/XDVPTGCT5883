using dang;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPostition;
    [SerializeField] private float delayBetweenShots = 2f;
    [SerializeField] private float damage = 2f;

    public float Damage { get; set; }
    public float DelayPershot { get; set; }

    private float _nextAttackTime;
    private ObjectPooling _pool;
    private TowerController _tower;
    private Bullet _currentBulletLoaded;

    void Start()
    {
        _pool = GetComponent<ObjectPooling>();
        _tower = GetComponent<TowerController>();

        Damage = damage;
        DelayPershot = delayBetweenShots;
        LoadProjectile();
    }

    void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime)
        {
            if (_tower.CurrentEnemyTarget != null && _currentBulletLoaded != null && _tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentBulletLoaded.transform.transform.parent = null;
                _currentBulletLoaded.SetEnemy(_tower.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + DelayPershot;
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
}
