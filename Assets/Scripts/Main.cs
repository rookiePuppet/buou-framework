using System;
using BuouFramework.AIControl.FiniteStateMachine;
using BuouFramework.Logging;
using BuouFramework.Schedule;
using BuouFramework.Tweening;
using BuouFramework.UI;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public float time;
        private StateMachine _stateMachine;

        private bool _isMoving = false;
        private bool _isJumping = false;

        private void Start()
        {
            var idleState = new IdleState();
            var moveState = new MoveState();
            var jumpState = new JumpState();

            _stateMachine = new StateMachine()
                .AddTransition(idleState, moveState, new FuncPredicate(() => _isMoving))
                .AddTransition(moveState, idleState, new FuncPredicate(() => !_isMoving))
                .AddAnyTransition(jumpState, new FuncPredicate(() => _isJumping))
                .AddTransition(jumpState, idleState, new FuncPredicate(() => !_isJumping))
                .SetCurrentState(idleState);
        }

        private async void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.N))
            {
                var now = DateTime.Now;
                TimerManager.CountDown(time).OnStopped(() => { Log.Info($"启动时间: {now:hh:mm:ss}", "计时结束"); });
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                UIManager.Instance.Open<MainView>();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIManager.Instance.ClearCache(true);
            }
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}