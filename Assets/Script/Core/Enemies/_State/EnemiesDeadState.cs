using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnemiesDeadState : IState
    {
        private EnemiesController enemiesController;

        public event UnityAction OnDead;

        public EnemiesDeadState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            enemiesController.animator.Play(EnumState.Dead.ToString());
        }

        public void StateUpdate()
        {
            OnDead?.Invoke();
        }

        public void Exit()
        {
            enemiesController.gameObject.SetActive(false);
        }
    }
}
