using UnityEngine;
using System.Collections;

namespace dang
{
    public class EnemiesDeadState : IState
    {
        private EnemyStateMachine stateMachine;
        private EnemiesController controller;

        public EnemiesDeadState(EnemyStateMachine stateMachine, EnemiesController controller)
        {
            this.stateMachine = stateMachine;
            this.controller = controller;
        }

        public void Enter()
        {
            controller.StopMovement();
            controller.StartCoroutine(DeathSequence());
        }

        public void StateUpdate() { }

        public void Exit()
        {
        }

        private IEnumerator DeathSequence()
        {
            controller.animator.Play(EnumState.Dead.ToString());
            yield return new WaitUntil(() =>
                controller.animator.GetCurrentAnimatorStateInfo(0).IsName(EnumState.Dead.ToString()) &&
                controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            );

            controller.animator.Play("Buried");
            yield return new WaitUntil(() =>
                controller.animator.GetCurrentAnimatorStateInfo(0).IsName("Buried") &&
                controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            );

            ObjectPooling.ReturnToPool(controller.gameObject);
        }
    }
}
