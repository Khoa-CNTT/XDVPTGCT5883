using System;
using System.Collections;
using UnityEngine;

namespace dang
{
    public class EnemiesController : MonoBehaviour
    {
        Enemy enemy;
        NpcStateMachine npcStateMachine;

        public void Awake()
        {
            enemy = new Enemy();
            npcStateMachine = new NpcStateMachine(this);
        }

        public void Start()
        {
            npcStateMachine.ChangeState(EnumState.Walk);
        }

        public void Update()
        {
            npcStateMachine.Update();
        }
    }
}
