using UnityEngine;

namespace dang
{
    public class EnemiesController : MonoBehaviour
    {
        // ========================= Enemy Stats ========================
        [Header("Enemy Stats")]
        public string entityType = "Blue";
        public int maxHealth = 100;
        public float runSpeed = 1f;

        // ========================= Enemy Waypoint ========================
        [Header("Enemy Waypoint")]
        [SerializeField] private Waypoint waypoint;
        private int currentWaypointIndex;

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
            enemyStateMachine.ChangeState(EnumState.Walk);
            currentWaypointIndex = 0;
        }

        public void Update()
        {
            enemyStateMachine.Update();
        }

        public void Move()
        {
            Vector3 currentPosition = waypoint.GetWaypointPosition(currentWaypointIndex);
            transform.position = Vector3.MoveTowards(transform.position, currentPosition, runSpeed * Time.deltaTime);
        }

        public void Hit()
        {

        }

        public void Dead()
        {

        }
    }
}
