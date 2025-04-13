using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnimiesWalkState : IState
    {
        private EnemyStateMachine statemachine;
        public event UnityAction OnMove;

        public EnimiesWalkState(EnemyStateMachine statemachine)
        {
            this.statemachine = statemachine;
        }

        public void Enter()
        {
            statemachine.enemiesController.animator.Play(EnumState.Run.ToString());
        }

        public void StateUpdate()
        {
            OnMove?.Invoke();
        }

        public void Exit()
        {
        }
    }
}
