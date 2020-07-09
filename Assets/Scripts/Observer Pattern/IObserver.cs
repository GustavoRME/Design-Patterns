using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface IObserver
    {
        void OnNotify(Entity entity, EventType eventType);    
    }

    public enum EventType
    {
        EntityFell,
        EntityVelocity,
        EntityTraveledDistance,
    }
}
