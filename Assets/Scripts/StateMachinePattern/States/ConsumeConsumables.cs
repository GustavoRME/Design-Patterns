using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class ConsumeConsumables : IState
    {
        private readonly Func<Consumable> _getConsumable;
        
        private readonly Action<Consumable> _setConsumable;
        
        private Consumable _consumable;        
        
        public ConsumeConsumables(Func<Consumable> getConsumable, Action<Consumable> setConsumable)
        {
            _getConsumable = getConsumable;
            _setConsumable = setConsumable;
        }

        public void OnEntry()
        {
            _consumable = _getConsumable?.Invoke();            
            _setConsumable?.Invoke(_consumable);
        }

        public void Tick() { }

        public void OnExit() => _consumable = null;
        
    }
}
