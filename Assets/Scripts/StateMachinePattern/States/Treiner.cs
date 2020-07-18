using StateMachineFinitePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class Treiner : IState
    {
        private readonly Animator _animator;
        private readonly string _attackProperty;

        private readonly Func<Dummy> _getDummy;
        private readonly Func<PrototypePattern.MeleeScriptable> _getArm;

        private Dummy _dummy;
        private PrototypePattern.MeleeScriptable _arm;
        private float _hitTime;

        public Treiner(Animator animator, string attackProperty, Func<Dummy> getDummy, Func<PrototypePattern.MeleeScriptable> getArm)
        {
            _animator = animator;
            _attackProperty = attackProperty;

            _getDummy = getDummy;
            _getArm = getArm;
        }

        public void OnEntry()
        {
            _dummy = _getDummy?.Invoke();
            _arm = _getArm?.Invoke();
        }

        public void OnExit()
        {
            _dummy = null;
            _arm = null;
        }

        public void Tick()
        {
            if (_hitTime == 0.0f || Time.time - _hitTime > _arm.delay)
            {
                Attack();
                SetAnimation();

                _hitTime = Time.time;
            }
        }   
        
        private void Attack() => _dummy.Hit(_arm.damage, _arm.canStun, _arm.canSlow, _arm.canPoison);
       
        private void SetAnimation() => _animator.SetTrigger(_attackProperty);
    }

}
