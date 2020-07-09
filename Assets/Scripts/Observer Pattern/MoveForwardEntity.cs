using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class MoveForwardEntity : Entity
    {
        Subject _subject;
        Rigidbody _rb;

        [SerializeField] float _desiredVelocity = 50f;
        [SerializeField] float _desiredDistanceTraveled = 100f;

        Vector3 _startPosition;
                
        bool _hasEntityVelocity;
        bool _hasEntityTraveledDistance;
        
        private void Awake()
        {
            _subject = new Subject();
            _rb = GetComponent<Rigidbody>();

            _startPosition = transform.position;            
        }

        private void Update()
        {
            if (_isActiveEntity)
                MoveForward();
        }

        public Subject EntityVelocity()
        {
            _hasEntityVelocity = true;

            return _subject;
        }
        
        public Subject EntityTravelledDistance()
        {
            _hasEntityTraveledDistance = true;

            return _subject;
        }
        
        public void SetEntityActive() => _isActiveEntity = true;
        
        private void MoveForward()
        {
            _rb.AddForce(Vector3.forward * 10f);

            if(_hasEntityVelocity)
            {
                //Check if reach the desired velocity
                if (_rb.velocity.sqrMagnitude > _desiredVelocity)
                    _subject.Notify(this, EventType.EntityVelocity);
            }

            if(_hasEntityTraveledDistance)
            {
                //Check if reach traveled distance
                if (Vector3.Distance(_startPosition, transform.position) > _desiredDistanceTraveled)
                    _subject.Notify(this, EventType.EntityTraveledDistance);
            }
        }
    }
}
