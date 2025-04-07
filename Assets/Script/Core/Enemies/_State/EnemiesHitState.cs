using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnemiesHitState : IState
    {
        private EnemiesController enemiesController;
        public event UnityAction OnHit;

        public EnemiesHitState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            enemiesController.animator.Play(EnumState.Hit.ToString());
        }

        public void StateUpdate()
        {
            OnHit?.Invoke();
        }

        public void Exit()
        {
            enemiesController.animator.Play(EnumState.Walk.ToString());
        }
    }
}
