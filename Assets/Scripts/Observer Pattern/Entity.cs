using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public abstract class Entity : MonoBehaviour
    {
        protected enum EntityShape 
        {
           Box,
           Sphere,
           Capsule
        }

        [SerializeField] protected EntityShape _entityShape;

        protected bool _isActiveEntity;
        
        public bool IsBox() => _entityShape == EntityShape.Box;
        public bool IsSphere() => _entityShape == EntityShape.Sphere;
        public bool IsCapsule() => _entityShape == EntityShape.Capsule;
    }
}
