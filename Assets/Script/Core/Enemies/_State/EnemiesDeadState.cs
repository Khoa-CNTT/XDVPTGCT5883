using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnemiesDeadState : IState
    {
        private EnemyStateMachine statemachine;

        public event UnityAction OnDead;

        public EnemiesDeadState(EnemyStateMachine statemachine)
        {
            this.statemachine = statemachine;
        }

        public void Enter()
        {
            statemachine.enemiesController.animator.Play(EnumState.Dead.ToString());
            statemachine.enemiesController.animator.GetCurrentAnimatorStateInfo(0).length.Equals(0);
            statemachine.enemiesController.animator.Play("Buried");
        }

        public void StateUpdate()
        {
            OnDead?.Invoke();
        }

        public void Exit()
        {
            statemachine.enemiesController.gameObject.SetActive(false);
        }
    }
}
