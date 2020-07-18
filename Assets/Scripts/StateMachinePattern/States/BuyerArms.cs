using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineFinitePattern
{
    public class BuyerArms : IState
    {
        private VenderMachineManager _venderMachine;

        private readonly Func<float> _getMoney;
        private readonly Action<PrototypePattern.MeleeScriptable> _setArm;

        private PrototypePattern.MeleeScriptable _purchasedArm;

        public BuyerArms(VenderMachineManager venderMachine, Func<float> getMoney, Action<PrototypePattern.MeleeScriptable> setArm)
        {
            _venderMachine = venderMachine;
            _getMoney = getMoney;
            _setArm = setArm;
        }

        public void OnEntry()
        {
            _purchasedArm = _venderMachine.BuyWeapon(_getMoney());
            _setArm?.Invoke(_purchasedArm);
        }

        public void OnExit() => _purchasedArm = null;

        public void Tick() { }        
    }

}
