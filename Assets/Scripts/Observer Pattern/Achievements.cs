using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class Achievements : MonoBehaviour, IObserver
    {
        [SerializeField] MoveForwardEntity _entity01 = null;
        [SerializeField] MoveForwardEntity _entity02 = null;
        [SerializeField] FallEntity _entity03 = null;

        bool _isUnlockedVelocitySphere;
        bool _isUnlockedTraveledDistanceCapsule;
        bool _isUnlockedFellAchievementBox;

        private void Start()
        {            
            _entity01.EntityVelocity().AddObserver(this);
            _entity02.EntityTravelledDistance().AddObserver(this);
            _entity03.EntityFell().AddObserver(this);
        }

        public void OnNotify(Entity entity, EventType eventType)
        {
            switch (eventType)
            {
                case EventType.EntityFell:
                    if (entity.IsBox() && !_isUnlockedFellAchievementBox)
                        UnlockBoxFellAchievement();
                    break;
                case EventType.EntityVelocity:
                    if (entity.IsSphere() && !_isUnlockedVelocitySphere)
                        UnlockSphereVelocityAchievement();
                    break;
                case EventType.EntityTraveledDistance:
                    if (entity.IsCapsule() && !_isUnlockedTraveledDistanceCapsule)
                        UnlockCapsuleTraveledAchievement();
                    break;             
            }
        }

        private void UnlockSphereVelocityAchievement()
        {
            _isUnlockedVelocitySphere = true;
            
            Debug.Log("Achivemenet Velocity Entity");
            
            _entity01.EntityVelocity().RemoveObserver(this);
        }

        private void UnlockCapsuleTraveledAchievement()
        {
            _isUnlockedTraveledDistanceCapsule = true;
            
            Debug.Log("Achivemenet Distance Traveled Entity");

            _entity02.EntityTravelledDistance().RemoveObserver(this);
        }

        private void UnlockBoxFellAchievement()
        {
            _isUnlockedFellAchievementBox = true;

            Debug.Log("Achivemenet Fell Entity");

            _entity03.EntityFell().RemoveObserver(this);
        }
    }
}

