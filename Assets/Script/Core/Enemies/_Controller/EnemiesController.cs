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

            enemyStateMachine.ChangeState(EnumState.Walk);

            currentWaypointIndex = 0;
        }

        public void Update()
        {
            enemyStateMachine.Update();
        }

        public void Walk()
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, runSpeed * Time.deltaTime);

            if (currentPositionReached())
            {
                UpdateCurrntPointIndex();
            }
        }

        private bool currentPositionReached()
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
            int lastWaypintIndex = waypoint.Points.Length - 1;
            if (currentWaypointIndex > lastWaypintIndex)
            {
                currentWaypointIndex++;
            }
        }

        public void Hit()
        {

        }

        public void Dead()
        {

        }
    }
}
