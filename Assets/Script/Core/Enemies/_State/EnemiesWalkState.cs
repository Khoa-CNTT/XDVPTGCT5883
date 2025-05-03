using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnimiesWalkState : IState
    {
        private EnemyStateMachine stateMachine;
        private EnemiesController controller;

        public EnimiesWalkState(EnemyStateMachine stateMachine, EnemiesController controller)
        {
            this.stateMachine = stateMachine;
            this.controller = controller;
        }

        public void Enter()
        {
            controller.ResumeMovement();
            controller.animator.Play(EnumState.Run.ToString());
        }

        public void StateUpdate()
        {
            controller.Run();
        }

        public void Exit()
        {
        }
    }
}
