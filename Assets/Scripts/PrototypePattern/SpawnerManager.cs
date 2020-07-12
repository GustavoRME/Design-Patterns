using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypePattern
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] GameObject _entity = null;
        [SerializeField] SpawnerScriptable _spawnData = null;

        private void Start()
        {
            SpawnEntity();
        }

        private void SpawnEntity()
        {
            int spawnIndex = 0;
            int instanceNumber = 1;
            int spawnLength = _spawnData.spawnPoints.Length;

            for (int i = 0; i < _spawnData.amountToSpawn; i++)
            {
                GameObject entity = Instantiate(_entity, _spawnData.spawnPoints[spawnIndex], Quaternion.identity);

                entity.name = _spawnData.prefabName + " (" + instanceNumber + ")";

                entity.SetActive(true);

                spawnIndex = (spawnIndex + 1) % spawnLength;

                instanceNumber++;
            }
        }       
    }
}
