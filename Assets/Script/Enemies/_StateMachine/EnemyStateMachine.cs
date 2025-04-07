using UnityEngine;
using System.Collections;

namespace dang
{
    public enum EnumState : byte
    {
        Idle = 0,
        Walk = 1,
        Wave = 2
    }

    public class NpcStateMachine : IStateMachine
    {
        private IState walkState;
        private IState hitState;
        private IState deadState;
        private IState currentState;
        EnemiesController enemiesController;

        public NpcStateMachine(EnemiesController enemiesController)
        {
            this.enemiesController = enemiesController;

            walkState = new EnimiesWalkState(this.enemiesController);
            hitState = new EnemiesHitState(this.enemiesController);
            deadState = new EnemiesDeadState(this.enemiesController);

            currentState = walkState;

            if (currentState != null)
            {
                currentState.Enter();
            }
        }

        public void ChangeState(EnumState EnewEState)
        {
            IState newState = null;
            switch (EnewEState)
            {
                case EnumState.Idle:
                    newState = walkState;
                    break;
                case EnumState.Walk:
                    newState = hitState;
                    break;
                case EnumState.Wave:
                    newState = deadState;
                    break;
            }
            if (newState == currentState) return;

            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            currentState.StateUpdate();
        }
    }
}
