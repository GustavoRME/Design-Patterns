using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachineFinitePattern
{
    public class MoveToTarget : IState
    {
        private readonly Func<Vector3> _getTarget;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly string _speedProperty;

        private Vector3 _target;

        public MoveToTarget(Func<Vector3> getTarget, NavMeshAgent agent, Animator animator, string speedProperty)
        {
            _getTarget = getTarget;            
            _agent = agent;
            _animator = animator;
            _speedProperty = speedProperty;
        }        

        public void OnEntry()
        {
            _target = _getTarget.Invoke();

            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.SetDestination(_target);

            _animator.SetFloat(_speedProperty, 1f);
        }

        public void Tick() { }                

        public void OnExit()
        {
            _agent.isStopped = true;
            _agent.enabled = false;

            _animator.SetFloat(_speedProperty, 0f);
        }
    }
}
