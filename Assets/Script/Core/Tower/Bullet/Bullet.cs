using dang;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;

    public TowerProjectile TowerOwner { get; set; }
    public float Damage { get; set; }

    private EnemiesController targetEnemy;

    void Update()
    {
        if (targetEnemy != null)
        {
            MoveBullet();
            RotateBullet();
        }
    }

    private void MoveBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
        float distanceToTarget = targetEnemy.transform.position.magnitude - transform.position.magnitude;

        if (distanceToTarget < minDistanceToDealDamage)
        {
            targetEnemy.EnemyHealth.DealDamage(Damage);
            TowerOwner.ResetTowerProjectile();
            ObjectPooling.ReturnToPool(gameObject);
        }
    }

    public void SetEnemy(EnemiesController enemy)
    {
        targetEnemy = enemy;
    }

    private void RotateBullet()
    {
        transform.right = targetEnemy.transform.position - transform.position;
    }

    public void ResetProjectile()
    {
        targetEnemy = null;
        transform.localRotation = Quaternion.identity;
    }
}
