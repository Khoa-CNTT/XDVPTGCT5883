using UnityEngine;

namespace dang
{
    public class EnimiesWalkState : IState
    {
        private EnemiesController enemiesController;

        public EnimiesWalkState(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;
        }

        public void Enter()
        {
            Debug.Log("Enter Walk State");
        }

        public void StateUpdate()
        {
            Debug.Log("Enter Walk Update");
        }

        public void Exit()
        {
            Debug.Log("Exit Walk State");
        }
    }
}
