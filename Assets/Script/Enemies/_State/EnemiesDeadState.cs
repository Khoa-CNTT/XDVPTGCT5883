using UnityEngine;

namespace dang
{
    public class EnemiesDeadState : IState
    {
        private EnemiesController enemiesController;

        public EnemiesDeadState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            Debug.Log("Enter Dead State");
        }

        public void StateUpdate()
        {
            Debug.Log("Dead State Update");
        }

        public void Exit()
        {
            Debug.Log("Exit Dead State");
        }
    }
}
