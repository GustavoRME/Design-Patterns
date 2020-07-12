using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypePattern
{
    [CreateAssetMenu(fileName = "Melee", menuName = "ScriptableObjects/Melee", order = 2)]
    public class MeleeScriptable : ScriptableObject
    {
        public int damage;        
        public float delay;

        [Header("Properties enables")]
        public bool canStun;
        public bool canSlow;
        public bool canPoison;

        [Header("Time for each properties")]
        public float stunTime;
        public float slowTime;
        public float poisonTime;

        [Space]
        public int poisonDamage;        
    }
}
