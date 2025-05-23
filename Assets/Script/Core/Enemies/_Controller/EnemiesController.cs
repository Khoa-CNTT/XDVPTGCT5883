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
        public static event UnityAction<EnemiesController> OnEndReached;
        private EnemyHealth enemyHealth;
        private SpriteRenderer spriteRenderer;
        private ObjectPooling pool;
        [HideInInspector] public EnemyHealth EnemyHealth { get; set; }
        private bool isRecorded = false;

        // ========================= Enemy Waypoint ========================
        [Header("Enemy Waypoint")]

        public Waypoint waypoint { get; set; }

        private int currentWaypointIndex;
        private Vector3 lastPointPos;
        private Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(currentWaypointIndex);

        // ========================= Enemy State Machine ========================
        [Header("Enemy State Machine")]
        public Animator animator;
        [HideInInspector] public EnemyStateMachine enemyStateMachine;

        public void Awake()
        {
            enemyStateMachine = new EnemyStateMachine(this);
        }

        public void Start()
        {
            animator = GetComponent<Animator>();
            enemyHealth = GetComponent<EnemyHealth>();
            pool = FindAnyObjectByType<ObjectPooling>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            EnemyHealth = GetComponent<EnemyHealth>();

            MoveSpeed = moveSpeed;

            currentWaypointIndex = 0;

            lastPointPos = transform.position;
        }

        public void Update()
        {
            enemyStateMachine.Update();
            enemyHealth.CalculateHealth();
        }

        public void Run()
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
            RotateSprite();

            if (CurrentPositionReached())
            {
                UpdateCurrentPointIndex();
            }
        }

        public void RotateSprite()
        {
            if (CurrentPointPosition.x > lastPointPos.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        private bool CurrentPositionReached()
        {
            float distanceToNextPointPosition = (transform.position - CurrentPointPosition).sqrMagnitude;
            if (distanceToNextPointPosition < 0.1f)
            {
                lastPointPos = transform.position;
                return true;
            }
            return false;
        }

        private void UpdateCurrentPointIndex()
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
            if (!isRecorded)
            {
                isRecorded = true;
                OnEndReached?.Invoke(this);
            }
            enemyHealth.ResetHealth();
            ObjectPooling.ReturnToPool(gameObject);
        }

        public void StopMovement()
        {
            MoveSpeed = 0f;
        }

        public void ResumeMovement()
        {
            MoveSpeed = moveSpeed;
        }

        public void Hit()
        {
            enemyStateMachine.ChangeState(EnumState.Hit);
        }

        public void Dead()
        {
            enemyStateMachine.ChangeState(EnumState.Dead);
        }

        public void ResetEnemy()
        {
            enemyStateMachine.ChangeState(EnumState.Run);
            currentWaypointIndex = 0;
            isRecorded = false;
        }
    }
}
