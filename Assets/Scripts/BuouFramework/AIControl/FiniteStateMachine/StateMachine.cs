using System;
using System.Collections.Generic;

namespace BuouFramework.AIControl.FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private readonly HashSet<Transition> _anyTransitions = new();
        private readonly Dictionary<Type, StateNode> _nodes = new();

        public void Update()
        {
            if (TryGetTransition(out var transition))
            {
                TransitionTo(transition.To);
            }

            _current?.State.Update();
        }

        public void FixedUpdate()
        {
            _current?.State.FixedUpdate();
        }

        public StateMachine AddTransition(IState fromState, IState toState, IPredicate condition)
        {
            GetOrAddNode(fromState).AddTransition(GetOrAddNode(toState).State, condition);
            return this;
        }

        public StateMachine AddAnyTransition(IState toState, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(toState).State, condition));
            return this;
        }

        public StateMachine SetCurrentState(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());
            _current = node ?? throw new NullReferenceException("No such state in machine.");
            _current?.State.Enter();

            return this;
        }

        public void TransitionTo(IState state)
        {
            var next = _nodes[state.GetType()];
            if (next == _current) return;

            _current.State?.Exit();
            next.State.Enter();

            _current = next;
        }
        
        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());
            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private bool TryGetTransition(out Transition transition)
        {
            foreach (var item in _anyTransitions)
            {
                if (item.Condition.Evaluate())
                {
                    transition = item;
                    return true;
                }
            }

            foreach (var item in _current.Transitions)
            {
                if (item.Condition.Evaluate())
                {
                    transition = item;
                    return true;
                }
            }

            transition = null;

            return false;
        }
    }
}