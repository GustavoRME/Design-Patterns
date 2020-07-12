using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypePattern
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "ScriptableObjects/Spawn", order = 0)]
    public class SpawnerScriptable : ScriptableObject
    {        
        public string prefabName;
    
        public int amountToSpawn;

        public Vector3[] spawnPoints;
    }
}
