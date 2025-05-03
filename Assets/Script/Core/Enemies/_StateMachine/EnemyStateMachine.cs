using UnityEngine;

namespace dang
{
    public enum EnumState
    {
        Run,
        Hit,
        Dead
    }

    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        private IState currentState;
        public EnemiesController Controller { get; }
        public EnimiesWalkState WalkState { get; private set; }
        public EnemiesHitState HitState { get; private set; }
        public EnemiesDeadState DeadState { get; private set; }

        public EnemyStateMachine(EnemiesController controller)
        {
            this.Controller = controller;

            WalkState = new EnimiesWalkState(this, controller);
            HitState = new EnemiesHitState(this, controller);
            DeadState = new EnemiesDeadState(this, controller);

            var initState = EnumState.Run;
            ChangeState(initState);
        }

        public void NewState(IState newState)
        {
            if (newState == currentState) return;

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void ChangeState(EnumState newEnumState)
        {
            switch (newEnumState)
            {
                case EnumState.Run:
                    NewState(WalkState);
                    break;
                case EnumState.Hit:
                    NewState(HitState);
                    break;
                case EnumState.Dead:
                    NewState(DeadState);
                    break;
            }
        }

        public void Update()
        {
            currentState?.StateUpdate();
        }
    }
}
