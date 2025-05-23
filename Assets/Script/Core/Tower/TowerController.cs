using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dang
{
    public class TowerController : MonoBehaviour
    {
        private Animator archerAnimator;
        public GameObject archerPos;
        private SpriteRenderer archerPosTransform;
        public TowerInit towerStats;
        public float AttackRange => towerStats.towerAttackRange;
        private bool isFacingLeft = false;

        [Header("Tower Attributes")]
        private List<EnemiesController> enemiesInRange;
        [HideInInspector] public EnemiesController CurrentEnemyTarget;
        public TowerUpgrade TowerUpgrade { get; set; }
        private string currentAnimation = "";

        IEnumerator Start()
        {
            enemiesInRange = new List<EnemiesController>();
            if (archerPos == null)
            {
                Debug.LogError("Không tìm thấy archerPos!");
            }

            towerStats = GetComponent<TowerInit>();
            if (towerStats == null)
            {
                Debug.LogError("Không tìm thấy TowerInit!");
            }

            yield return null;
            archerAnimator = archerPos.GetComponentInChildren<Animator>();
            if (archerAnimator == null)
            {
                Debug.LogError("Không tìm thấy Animator của Archer!");
            }

            archerPosTransform = archerPos.GetComponentInChildren<SpriteRenderer>();
            if (archerPosTransform == null)
            {
                Debug.LogError("Không tìm thấy SpriteRenderer của Archer!");
            }

            TowerUpgrade = GetComponent<TowerUpgrade>();
        }

        void Update()
        {
            GetCurerntEnemyTarget();
            RotateTowardsTarget();
        }

        public void InjectData(TowerData data)
        {
            if (towerStats == null)
            {
                towerStats = GetComponent<TowerInit>();
            }

            towerStats.SetData(data);
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

        private void RotateTowardsTarget()
        {
            if (archerAnimator == null) return;

            if (CurrentEnemyTarget == null)
            {
                if (currentAnimation != "Idle")
                {
                    archerAnimator.Play("Idle");
                    currentAnimation = "Idle";
                }

                return;
            }

            Vector3 direction = CurrentEnemyTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            string directionAnim = GetAnimationDirection(angle);

            if (currentAnimation != directionAnim)
            {
                archerAnimator.Play(directionAnim);
                currentAnimation = directionAnim;

                if (currentAnimation != "Idle")
                {
                    archerAnimator.speed = towerStats.towerAttackSpeed / 2;
                }
            }

            if (directionAnim == "Shoot Front Mirror" && !isFacingLeft)
            {
                archerPosTransform.transform.localScale = new Vector3(-1, 1, 1);
                isFacingLeft = true;
            }
            else if (directionAnim != "Shoot Front Mirror" && isFacingLeft)
            {
                archerPosTransform.transform.localScale = new Vector3(1, 1, 1);
                isFacingLeft = false;
            }
        }

        public void ReturnToIdle()
        {
            if (archerAnimator == null) return;

            if (CurrentEnemyTarget != null)
            {
                archerAnimator.Play("Idle");
                currentAnimation = "Idle";
                if (currentAnimation == "Idle")
                {
                    archerAnimator.speed = 1f;
                }
            }
        }

        private string GetAnimationDirection(float angle)
        {
            angle = (angle + 360f) % 360f;

            if (angle >= 315f || angle < 45f)
            {
                return "Shoot Front";
            }
            else if (angle >= 45f && angle < 135f)
            {
                return "Shoot Up";
            }
            else if (angle >= 135f && angle < 225f)
            {
                return "Shoot Front Mirror";
            }
            else
            {
                return "Shoot Down";
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            EnemiesController newEnemy = collision.GetComponent<EnemiesController>();
            if (collision.CompareTag("Enemy"))
            {
                enemiesInRange.Add(newEnemy);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemiesController enemy = collision.GetComponent<EnemiesController>();
                if (enemiesInRange.Contains(enemy))
                {
                    enemiesInRange.Remove(enemy);
                    CurrentEnemyTarget = null;
                }
            }
        }
    }
}
