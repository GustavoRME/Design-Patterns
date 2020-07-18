using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class ConsumablesResearcher : IState
    {
        private Consumable _consumable;
        
        private readonly Transform _entity;
        private readonly Action<Consumable> _setConsumable;

        public ConsumablesResearcher(Transform entity, Action<Consumable> setConsumable)
        {
            _entity = entity;
            _setConsumable = setConsumable;
        }

        public void OnEntry()
        {
            SearchConsumable();
            _setConsumable?.Invoke(_consumable);
        }

        public void OnExit() => _consumable = null;

        public void Tick() { }

        private void SearchConsumable()
        {
            Consumable[] consumables = UnityEngine.Object.FindObjectsOfType<Consumable>();

            float currentDistance = float.MaxValue;

            foreach (Consumable consumable in consumables)
            {
                float distance = Vector3.Distance(_entity.position, consumable.transform.position);

                if(distance < currentDistance)
                {
                    _consumable = consumable;
                    currentDistance = distance;
                }
            }
        }
    
    }
}

