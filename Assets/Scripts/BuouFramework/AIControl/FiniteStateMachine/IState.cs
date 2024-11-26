namespace BuouFramework.AIControl.FiniteStateMachine
{
    public interface IState
    {
        void Enter();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}