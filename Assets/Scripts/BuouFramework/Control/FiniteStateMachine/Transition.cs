﻿namespace BuouFramework.Control.FiniteStateMachine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }

    public class Transition : ITransition
    {
        public IState To { get; }
        public IPredicate Condition { get; }

        public Transition(IState to, IPredicate predicate)
        {
            To = to;
            Condition = predicate;
        }
    }
}