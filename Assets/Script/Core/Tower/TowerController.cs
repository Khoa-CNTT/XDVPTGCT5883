using System.Collections.Generic;
using UnityEngine;

namespace dang
{
    public class TowerController : MonoBehaviour
    {
        private Animator archerAnimator;

        [Header("Tower Attributes")]
        private List<Enemy> enemiesInRange;
        public Enemy CurrentEnemyTarget;

        void Start()
        {
            enemiesInRange = new List<Enemy>();

            archerAnimator = transform.Find("ArcherPos").GetComponentInChildren<Animator>();
            if (archerAnimator == null)
            {
                Debug.LogError("Không tìm thấy Animator của Archer!");
            }
        }

        void Update()
        {
            GetCurerntEnemyTarget();
            RotateTowardsTarget();
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
                Debug.Log($"Enemy Target: {CurrentEnemyTarget.name}");
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            if (collision.CompareTag("Enemy"))
            {
                enemiesInRange.Add(newEnemy);
            }
        }

        private void RotateTowardsTarget()
        {
            if (CurrentEnemyTarget == null || archerAnimator == null) return;

            Vector3 direction = CurrentEnemyTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            string directionAnim = GetAnimationDirection(angle);
            archerAnimator.Play(directionAnim);
            Debug.Log($"Playing Animation: {directionAnim}");
        }

        private string GetAnimationDirection(float angle)
        {
            if (angle >= -22.5f && angle <= 22.5f)
                return "Shoot Front";
            else if (angle > 22.5f && angle <= 67.5f)
                return "Shoot Diagonal Down";
            else if (angle > 67.5f && angle <= 112.5f)
                return "Shoot Down";
            else if (angle > 112.5f && angle <= 157.5f)
                return "Shoot Diagonal Up";
            else
                return "Shoot Up";
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemiesInRange.Contains(enemy))
                {
                    enemiesInRange.Remove(enemy);
                    CurrentEnemyTarget = null;
                }
            }
        }
    }
}
