using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnimiesWalkState : IState
    {
        private EnemiesController enemiesController;
        public event UnityAction OnMove;

        public EnimiesWalkState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            enemiesController.animator.Play(EnumState.Walk.ToString());
        }

        public void StateUpdate()
        {
            OnMove?.Invoke();
        }

        public void Exit()
        {
            enemiesController.animator.Play(EnumState.Hit.ToString());
        }
    }
}
