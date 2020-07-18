using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachineFinitePattern
{
    public class DummyChooser : IState
    {
        private readonly Dummy[] _dummies;
        private Dummy _dummy;

        private readonly Transform _entity;
        private readonly NavMeshAgent _agent;
        private readonly Animator _animator;
        private readonly string _speedProperty;

        private readonly Action<Dummy> _setDummy;

        public DummyChooser(Transform entity, Action<Dummy> setDummy, NavMeshAgent agent, Animator animator , string speedProperty ,params Dummy[] dummies)
        {
            _entity = entity;
            _dummies = dummies;
            _agent = agent;
            _animator = animator;
            _speedProperty = speedProperty;
            _setDummy = setDummy;
        }

        public void OnEntry()
        {
            ChooseClosetDummy();

            _agent.enabled = true;
            _agent.isStopped = false;
            _agent.SetDestination(_dummy.Position);

            _setDummy?.Invoke(_dummy);

            _animator.SetFloat(_speedProperty, 1f);
        }

        public void Tick() { }       

        public void OnExit()
        {
            _agent.isStopped = true;
            _agent.enabled = false;
            _animator.SetFloat(_speedProperty, 0f);
        }
        
        private void ChooseClosetDummy()
        {
            _dummy = _dummies[0];
            
            float currentDistance = Vector3.Distance(_dummy.Position, _entity.position);

            for (int i = 1; i < _dummies.Length; i++)
            {
                float distance = Vector3.Distance(_dummies[i].Position, _entity.position);

                if (distance < currentDistance)
                {
                    _dummy = _dummies[i];
                    currentDistance = distance;
                }
            }
            
        }
    }
}
