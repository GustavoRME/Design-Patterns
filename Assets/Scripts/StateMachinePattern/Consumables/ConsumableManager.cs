using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class ConsumableManager : MonoBehaviour
    {   
        [SerializeField] private float _timeToSpawn = 10f;

        [SerializeField] private bool _canSpawn = true;
        [SerializeField] private bool _enableAtStart = true;

        [SerializeField] private Consumable[] _consumables = null;

        private int _currentConsumableIndex;

        private void Awake()
        {
            foreach (var consumable in _consumables)
                consumable.Disable();
            
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            if (_enableAtStart)
            {
                _consumables[_currentConsumableIndex].Enable();
                _currentConsumableIndex = (_currentConsumableIndex + 1) % _consumables.Length;
            }

            while(_canSpawn)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                
                _consumables[_currentConsumableIndex].Enable();

                _currentConsumableIndex = (_currentConsumableIndex + 1) % _consumables.Length;

            }
        }    
    }
}
