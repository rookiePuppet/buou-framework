using System.Collections.Generic;

namespace BuouFramework.Control.FiniteStateMachine
{
    public class StateNode
    {
        public IState State { get; private set; }
        public HashSet<Transition> Transitions { get; }

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<Transition>();
        }

        public void AddTransition(IState toState, IPredicate condition)
        {
            var transition = new Transition(toState, condition);
            Transitions.Add(transition);
        }
    }
}