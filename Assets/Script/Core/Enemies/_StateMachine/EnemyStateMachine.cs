namespace dang
{
    public enum EnumState
    {
        Run,
        Hit,
        Dead
    }

    public class EnemyStateMachine : IStateMachine
    {
        private IState walkState;
        private IState hitState;
        private IState deadState;
        private IState currentState;

        public EnemiesController enemiesController;

        public EnemyStateMachine(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;

            InitializeStates();

            RegisterEvent();
        }

        private void InitializeStates()
        {
            walkState = new EnimiesWalkState(this);
            hitState = new EnemiesHitState(this);
            deadState = new EnemiesDeadState(this);

            currentState = walkState;

            if (currentState != null)
            {
                currentState.Enter();
            }
        }

        private void RegisterEvent()
        {
            if (walkState is EnimiesWalkState WalkStateInstance)
            {
                WalkStateInstance.OnMove += enemiesController.Walk;
            }

            if (hitState is EnemiesHitState HitStateInstance)
            {
                HitStateInstance.OnHit += enemiesController.Hit;
            }

            if (deadState is EnemiesDeadState DeadStateInstance)
            {
                DeadStateInstance.OnDead += enemiesController.Dead;
            }
        }

        public void ChangeState(EnumState EnewEState)
        {
            IState newState = null;
            switch (EnewEState)
            {
                case EnumState.Run:
                    newState = walkState;
                    break;
                case EnumState.Hit:
                    newState = hitState;
                    break;
                case EnumState.Dead:
                    newState = deadState;
                    break;
            }

            if (newState == currentState) return;

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            currentState.StateUpdate();
        }
    }
}
