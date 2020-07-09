using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class EnvironmentAudio : MonoBehaviour, IObserver
    {
        [SerializeField] MoveForwardEntity _entity01 = null;
        [SerializeField] FallEntity _entity02 = null;

        private void Start()
        {
            _entity01.EntityVelocity().AddObserver(this);
            _entity02.EntityFell().AddObserver(this);
        }

        public void OnNotify(Entity entity, EventType eventType)
        {
            switch (eventType)
            {
                case EventType.EntityFell:
                    if (entity.IsBox())
                        FallSound();
                    break;
                case EventType.EntityVelocity:
                    if (entity.IsSphere())
                        CrossedSoundBarrier();                    
                    break;             
            }
        }

        private void CrossedSoundBarrier() 
        { 
            Debug.Log("Crossed the sound barrier");

            _entity01.EntityVelocity().RemoveObserver(this);
        } 
        
        private void FallSound()
        {
            Debug.Log("Fell sound");

            _entity02.EntityFell().RemoveObserver(this);
        }
    }
}
