using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace dang
{
    public class EnemiesHitState : IState
    {
        private EnemyStateMachine statemachine;
        public event UnityAction OnHit;

        public EnemiesHitState(EnemyStateMachine statemachine)
        {
            this.statemachine = statemachine;
        }

        public void Enter()
        {

        }

        public void StateUpdate()
        {
            OnHit?.Invoke();
        }

        public void Exit()
        {

        }
    }
}
