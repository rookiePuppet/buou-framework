using System;

namespace BuouFramework.AIControl.FiniteStateMachine
{
    public interface IPredicate
    {
        bool Evaluate();
    }

    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate() => _func();
    }
}