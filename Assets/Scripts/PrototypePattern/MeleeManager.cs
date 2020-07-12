using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypePattern
{
    public class MeleeManager : MonoBehaviour
    {
        [SerializeField] MeleeScriptable _meleeData = null;        
        
        [SerializeField] Transform target = null;

        [SerializeField] bool _useAttack = false;

        float _clickTime;

        int _enemyLife = 10;                

        private void Update()
        {            
            if(_useAttack)
            {
                if (Time.time - _clickTime > _meleeData.delay || _clickTime == 0)
                    Attack();
            }
        }
        
        private void Attack()
        {
            _enemyLife -= _meleeData.damage;
            _clickTime = Time.time;
            _useAttack = false;

            string message = "You inflict " + _meleeData.damage + " at " + target.name;

            if (_meleeData.canStun)
                message += ", you too stunned him for " + _meleeData.stunTime;
            
            if (_meleeData.canPoison)
                message += ", you too poisoned him for " + _meleeData.poisonTime;

            if (_meleeData.canSlow)
                message += ", you too slowed him for " + _meleeData.slowTime;

            message += ". Enemy stay with " + _enemyLife + " life";
            Debug.Log(message);            
        }
    }
}
