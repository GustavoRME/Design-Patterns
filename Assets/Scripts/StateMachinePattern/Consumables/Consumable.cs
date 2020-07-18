using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class Consumable : MonoBehaviour
    {
        public enum ConsumeType 
        { 
            Money, 
            Potion, 
            Tonic
        };

        [SerializeField] private ConsumeType _consumeType = ConsumeType.Money;

        public int amount = 0;

        public Vector3 Positon => transform.position;

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);

        public ConsumeType Consume()
        {
            Disable();

            return _consumeType;
        }
    }
}
