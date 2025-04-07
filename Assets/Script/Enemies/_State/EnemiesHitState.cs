using UnityEngine;

namespace dang
{
    public class EnemiesHitState : IState
    {
        private EnemiesController enemiesController;

        public EnemiesHitState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            Debug.Log("Enter Hit State");
        }

        public void StateUpdate()
        {
            Debug.Log("Hit State Update");
        }

        public void Exit()
        {
            Debug.Log("Exit Hit State");
        }
    }
}
