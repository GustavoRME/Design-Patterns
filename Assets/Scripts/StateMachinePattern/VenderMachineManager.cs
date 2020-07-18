using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class VenderMachineManager : MonoBehaviour
    {
        [Header("Staff")]
        [SerializeField] private PrototypePattern.MeleeScriptable _staffMelee = null;
        [SerializeField] private float _staffPrice = 0.0f;
        [SerializeField] private Transform _staffPosition = null;
        private bool _isStaffAvailable = true;

        [Header("Legendary")]
        [SerializeField] private PrototypePattern.MeleeScriptable _rakeLegendary = null;
        [SerializeField] private float _legendaryPrice = 0.0f;
        [SerializeField] private Transform _legendaryPosition = null;
        private bool _isLegendaryAvailable = true;


        public bool CanBuyWeapon(float money)
        {
            if(_isStaffAvailable)
            {
                return money >= _staffPrice;
            }
            else if(_isLegendaryAvailable)
            {
                return money >= _legendaryPrice;
            }

            return false;
        }

        public PrototypePattern.MeleeScriptable BuyWeapon(float money)
        {
            PrototypePattern.MeleeScriptable melee = null;

            if (money >= _legendaryPrice && _isLegendaryAvailable)
            {
                melee = _rakeLegendary;
                _isLegendaryAvailable = false;
                _legendaryPosition.gameObject.SetActive(false);
            }
            else if(money >= _staffPrice && _isStaffAvailable)
            {
                melee = _staffMelee;
                _isStaffAvailable = false;
                _staffPosition.gameObject.SetActive(false);
            }
                       
            return melee;
        }             
    }
}
