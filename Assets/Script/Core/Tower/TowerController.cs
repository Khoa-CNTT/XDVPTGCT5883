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
        private TowerInit towerStats;

        [Header("Tower Attributes")]
        private List<EnemiesController> enemiesInRange;
        [HideInInspector] public EnemiesController CurrentEnemyTarget;

        private string currentAnimation = "";

        IEnumerator Start()
        {
            enemiesInRange = new List<EnemiesController>();
            if (archerPos == null)
            {
                archerPos = GameObject.Find("ArcherPos");
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
        }

        void Update()
        {
            GetCurerntEnemyTarget();
            RotateTowardsTarget();

            if (CurrentEnemyTarget == null && archerAnimator != null)
            {
                AnimatorStateInfo stateInfo = archerAnimator.GetCurrentAnimatorStateInfo(0);
                if (currentAnimation != "Idle" && stateInfo.normalizedTime >= 1f)
                {
                    archerAnimator.Play("Idle");
                    currentAnimation = "Idle";
                    archerAnimator.speed = 1f; // <- Thêm dòng này
                }
            }
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
            if (CurrentEnemyTarget == null || archerAnimator == null) return;

            Vector3 direction = CurrentEnemyTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            string directionAnim = GetAnimationDirection(angle);

            AnimatorStateInfo stateInfo = archerAnimator.GetCurrentAnimatorStateInfo(0);
            if (currentAnimation != directionAnim && stateInfo.normalizedTime >= 1f)
            {
                archerAnimator.Play(directionAnim);
                currentAnimation = directionAnim;
                if (currentAnimation != "Idle")
                {
                    archerAnimator.speed = towerStats.towerAttackSpeed / 6;
                }

                // Debug.Log($"Playing Animation: {directionAnim}");
            }

            // if (angle > 270f && angle <= 90f)
            // {
            //     archerPosTransform.flipX = true;
            // }
            // else
            // {
            //     archerPosTransform.flipX = false;
            // }
        }

        private string GetAnimationDirection(float angle)
        {
            angle = (angle + 360f) % 360f;

            if (angle >= 345f || angle < 15f)
                return "Shoot Front";
            else if (angle >= 15f && angle < 45f)
                return "Shoot Diagonal Up";
            else if (angle >= 45f && angle < 75f)
                return "Shoot Up";
            else if (angle >= 75f && angle < 105f)
                return "Shoot Diagonal Up";
            else if (angle >= 105f && angle < 165f)
                return "Shoot Front";
            else if (angle >= 165f && angle < 195f)
                return "Shoot Diagonal Down";
            else if (angle >= 195f && angle < 255f)
                return "Shoot Down";
            else if (angle >= 255f && angle < 285f)
                return "Shoot Diagonal Down";
            else if (angle >= 285f && angle < 315f)
                return "Shoot Front";
            else if (angle >= 315f && angle < 345f)
                return "Shoot Diagonal Up";
            else
                return "Shoot Front";
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
