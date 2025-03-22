using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace dang
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float attackRange = 3f;
        private bool gameStarted;
        private List<Enemy> enemiesInRange;
        public Enemy CurrentEnemyTarget { get; set; }

        void Start()
        {
            gameStarted = true;
            enemiesInRange = new List<Enemy>();
        }

        void Update()
        {
            GetCurerntEnemyTarget();
        }

        private void GetCurerntEnemyTarget()
        {
            if (enemiesInRange.Count <= 0)
            {
                CurrentEnemyTarget = null;
                return;
            }
            else
            {
                CurrentEnemyTarget = enemiesInRange[0];
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy newEnemy = collision.GetComponent<Enemy>();
                enemiesInRange.Add(newEnemy);
            }
        }

        void OnDrawGizmos()
        {
            if (!gameStarted)
            {
                GetComponent<CircleCollider2D>().radius = attackRange;
            }
            Gizmos.DrawWireSphere(transform.position, attackRange / 1.33f);
        }
    }
}