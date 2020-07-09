
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class Subject
    {        
        readonly List<IObserver> _observers;        

        public Subject()
        {
            _observers = new List<IObserver>();
        }

        //Notify all observer on the list
        public void Notify(Entity entity, EventType eventType)
        {
            //How we are removing from the list the observers when they are notifying by the event they are observing
            //We need create one copy from the list. 
            List<IObserver> observersTemp = new List<IObserver>(_observers);            
            
            for (int i = 0; i < observersTemp.Count; i++)
            {
                observersTemp[i].OnNotify(entity, eventType);
            }
        }

        //Add observer
        public void AddObserver(IObserver observer) => _observers.Add(observer);

        //Remove observer
        public void RemoveObserver(IObserver observer) => _observers.Remove(observer);                
    }

}
