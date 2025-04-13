using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnemiesController : MonoBehaviour
    {
        // ========================= Enemy Stats ========================
        [Header("Enemy Stats")]
        public string entityType = "Blue";
        public int maxHealth = 100;
        public float moveSpeed = 1f;
        public float MoveSpeed { get; set; }
        public static event UnityAction OnEndReached;
        private EnemyHealth enemyHealth;
        private ObjectPooling pool;

        // ========================= Enemy Waypoint ========================
        [Header("Enemy Waypoint")]

        public Waypoint waypoint { get; set; }

        private int currentWaypointIndex;
        private Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(currentWaypointIndex);

        // ========================= Enemy State Machine ========================
        [Header("Enemy State Machine")]
        public Animator animator;
        public EnemyStateMachine enemyStateMachine;

        public void Awake()
        {
            enemyStateMachine = new EnemyStateMachine(this);
        }

        public void Start()
        {

            animator = GetComponent<Animator>();
            enemyHealth = GetComponent<EnemyHealth>();
            pool = FindAnyObjectByType<ObjectPooling>();

            MoveSpeed = moveSpeed;

            currentWaypointIndex = 0;
        }

        public void Update()
        {
            enemyStateMachine.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                enemyHealth.DealDamage(5f);
            }
            enemyHealth.CalculateHealth();
        }

        public void Walk()
        {
            enemyStateMachine.ChangeState(EnumState.Run);
            transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);

            if (CurrentPositionReached())
            {
                UpdateCurrntPointIndex();
            }
        }

        private bool CurrentPositionReached()
        {
            float distanceToNextPointPosition = (transform.position - CurrentPointPosition).sqrMagnitude;
            if (distanceToNextPointPosition < 0.1f)
            {
                return true;
            }
            return false;
        }

        private void UpdateCurrntPointIndex()
        {
            int lastWaypointIndex = waypoint.Points.Length - 1;
            if (currentWaypointIndex < lastWaypointIndex)
            {
                currentWaypointIndex++;
            }

            else if (currentWaypointIndex == lastWaypointIndex)
            {
                EndPointReached();
            }
        }

        private void EndPointReached()
        {
            OnEndReached?.Invoke();
            enemyHealth.ResetHealth();
            ObjectPooling.ReturnToPool(gameObject);
        }

        public void StopMovement()
        {
            MoveSpeed = 0f;
        }

        public void ResumeMOvement()
        {
            MoveSpeed = moveSpeed;
        }

        public void Hit()
        {
            StartCoroutine(HitCoroutine());
        }

        private IEnumerator HitCoroutine()
        {
            enemyStateMachine.ChangeState(EnumState.Hit);
            StopMovement();
            yield return new WaitForSeconds(0.5f);
            Walk();
        }

        public void Dead()
        {
            if (!enemyHealth.isDead) return;
            else
                enemyStateMachine.ChangeState(EnumState.Dead);
        }

        public void ResetEnemy()
        {
            currentWaypointIndex = 0;
        }
    }
}
