using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightPattern
{
    public class World : MonoBehaviour
    {
        //Size from the plane
        const int PLANE_SIZE = 10;

        [Header("Flyweight")]

        //It's only mesh to all terrains. 
        [SerializeField] Mesh _terrainMesh = null;
        [SerializeField] Material _terrainMaterial = null;

        [Header("Textures")]
        //To texture has several terrains
        [SerializeField] Texture _grassTexture = null;
        [SerializeField] Texture _dryLeavesTexture = null;
        [SerializeField] Texture _crackedTexture = null;
        [SerializeField] Texture _stoneTexture = null;

        [Space]

        [Tooltip("Amount terrain on width")]
        [SerializeField] int _widthCount = 10000;

        [Tooltip("Amount terrain on height")]
        [SerializeField] int _heightCount = 10000;

        //Array of terrains 
        Terrain[,] _terrains;

        //Types of terrain
        Terrain _grassTerrain;
        Terrain _dryLeavesTerrain;
        Terrain _crackedTerrain;
        Terrain _stoneTerrain;

        private void Awake()
        {
            //Create all preset from terrain
            _grassTerrain = new Terrain(2, false, _terrainMesh, _grassTexture, _terrainMaterial);
            _dryLeavesTerrain = new Terrain(1, true, _terrainMesh, _dryLeavesTexture, _terrainMaterial);
            _crackedTerrain = new Terrain(1, true, _terrainMesh, _crackedTexture, _terrainMaterial);
            _stoneTerrain = new Terrain(3, true, _terrainMesh, _stoneTexture, _terrainMaterial);

            //Create the terrain wolrd
            _terrains = new Terrain[_widthCount, _heightCount];

            //Procedural terrain
            for (int i = 0; i < _widthCount; i++)
            {                
                for (int j = 0; j < _heightCount; j++)
                {
                    int rand = Random.Range(0, 4);

                    //0 -> grass
                    //1 -> dryLeaves
                    //2 -> cracked
                    //3 -> stone

                    if (rand == 0)
                    {
                        _terrains[i, j] = _grassTerrain;
                        CreateGameObject("Grass", _terrains[i, j], i, j);
                    }
                    else if(rand == 1)
                    {
                        _terrains[i, j] = _dryLeavesTerrain;
                        CreateGameObject("Dry Leaves", _terrains[i, j], i, j);
                    }
                    else if(rand == 2)
                    {
                        _terrains[i, j] = _crackedTerrain;                        
                        CreateGameObject("Cracked", _terrains[i, j], i, j);
                    }
                    else if(rand == 3)
                    {
                        _terrains[i, j] = _stoneTerrain;
                        CreateGameObject("Stone", _terrains[i, j], i, j);
                    }                    
                }
            }
        }

        private void CreateGameObject(string name, Terrain terrain, int x, int y)
        {
            GameObject go = new GameObject(name + " Terrain", typeof(MeshRenderer), typeof(MeshFilter));

            //Set Default MeshFilter from terrian mesh
            go.GetComponent<MeshFilter>().mesh = terrain.GetMesh();

            //Set material from terrain material
            go.GetComponent<MeshRenderer>().material = terrain.GetMaterial();            

            //Set a new Texture to this mesh renderer with property block passing the same mesh renderer.
            go.GetComponent<MeshRenderer>().SetPropertyBlock(terrain.SetTextureFromPropertyBlock(go.GetComponent<MeshRenderer>()));

            //Set the new position to terrain. 
            go.transform.position = new Vector3(PLANE_SIZE * x, 0f, PLANE_SIZE * y);

            //To organize the hierarchy on the inspector. Set the parent transform with this object
            go.transform.SetParent(transform);
        }
    }

}
