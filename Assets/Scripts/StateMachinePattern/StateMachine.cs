using StateMachineFinitePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class StateMachine
    {
        private Dictionary<IState, List<Transition>> _transitions;                  //Cache all transitions and states to go to
        private List<Transition> _currentTransition;                                //Cache the current transition from the current state
        private List<Transition> _anyStateTransition;                               //Cache the transition with which came from any state

        private static List<Transition> _emptyTransition = new List<Transition>(); //Used only cause the state don't have any trasition, and we can still doing iterator without any problems.

        private IState _currentState;

        public StateMachine()
        {
            _transitions = new Dictionary<IState, List<Transition>>();
            _currentTransition = new List<Transition>();
            _anyStateTransition = new List<Transition>();

            _currentState = null;
        }

        /// <summary>
        /// It's reponsible for call the tick from the current state and check if can go to next state through transition
        /// </summary>
        public void Tick()
        {
            IState to = CheckTransition();

            SetState(to);
            
            _currentState?.Tick();
        }

        /// <summary>
        /// Add a new transition. Set up where came from, to go on and the condition to go to the state
        /// </summary>
        /// <param name="from">State where it came from</param>
        /// <param name="to">State where it go on</param>
        /// <param name="predicate">Condition to go to state</param>
        public void AddTrasition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from, out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
            }

            transitions.Add(new Transition(to, predicate));
            _transitions[from] = transitions;
        }

        /// <summary>
        /// Add a new transition to go to a next state. Don't metter what is the current transition
        /// </summary>
        /// <param name="to">State to go to</param>
        /// <param name="predicate">Condition to go to that state</param>
        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            _anyStateTransition.Add(new Transition(to, predicate));
        }

        /// <summary>
        /// Set start state
        /// </summary>
        /// <param name="entryState"></param>
        public void SetEntryState(IState entryState)
        {
            SetState(entryState);
        }

        private void SetState(IState to)
        {
            if (to == _currentState || to == null)
                return;

            _currentState?.OnExit();
            _currentState = to;

            _currentTransition = _transitions[_currentState];

            _currentState?.OnEntry();
        }

        private IState CheckTransition()
        {
            foreach (var transition in _anyStateTransition)
            {
                if (transition.Condition())
                    return transition.To;
            }

            foreach (var transition in _currentTransition)
            {
                if (transition.Condition())
                    return transition.To;
            }

            return null;
        }

        private class Transition
        {
            public IState To { get; }
            public Func<bool> Condition { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        } //Class that cache state to go to and the condition to go that state
    }
}
