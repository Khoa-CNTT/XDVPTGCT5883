using UnityEngine;

namespace dang
{
    public class EnemiesHitState : IState
    {
        private EnemyStateMachine stateMachine;
        private EnemiesController controller;
        private float hitDuration = 0.5f;
        private float timer;
        private bool hasChangedState = false;

        public EnemiesHitState(EnemyStateMachine stateMachine, EnemiesController controller)
        {
            this.stateMachine = stateMachine;
            this.controller = controller;
        }

        public void Enter()
        {
            timer = 0;
            hasChangedState = false;
            controller.StopMovement();
        }

        public void StateUpdate()
        {
            if (hasChangedState) return;

            timer += Time.deltaTime;

            if (timer >= hitDuration)
            {
                hasChangedState = true;
                stateMachine.ChangeState(EnumState.Run);
            }
        }

        public void Exit()
        {
        }
    }
}
