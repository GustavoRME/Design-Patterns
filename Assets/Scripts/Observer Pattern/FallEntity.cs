using ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class FallEntity : Entity
    {
        Subject _subject;
        Rigidbody _rb;
    
        private void Awake()
        {
            _subject = new Subject();
            _rb = GetComponent<Rigidbody>();
        }

        public Subject EntityFell() => _subject;
        public void FallEntityTrigger()
        {
            _rb.useGravity = true;
            _subject.Notify(this, EventType.EntityFell);            
        }
    }
}
