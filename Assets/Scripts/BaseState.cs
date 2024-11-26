using BuouFramework.AIControl.FiniteStateMachine;
using BuouFramework.Logging;
using UnityEngine;

namespace Game
{
    public class BaseState : IState
    {
        public virtual void Enter()
        {
            // noop
        }

        public virtual void Update()
        {
            // noop
        }

        public virtual void FixedUpdate()
        {
            // noop
        }

        public virtual void Exit()
        {
            // noop
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter()
        {
            base.Enter();
            Log.Info("Enter IdleState");
        }
    }

    public class MoveState : BaseState
    {
        public override void Enter()
        {
            base.Enter();
            Log.Info("Enter MoveState");
        }
    }

    public class JumpState : BaseState
    {
        public override void Enter()
        {
            base.Enter();

            Log.Info("Enter JumpState");
        }
    }
}