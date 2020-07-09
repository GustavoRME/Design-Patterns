using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace FlyweightPattern
{
    public class Terrain
    {
        Mesh _mesh;
        MaterialPropertyBlock _propBlock;
        Texture _texture;
        Material _material;
        
        int _cost;
        bool _isAsphalt;

        public Terrain(int cost, bool isAsphalt, Mesh mesh, Texture texture, Material material)
        {
            _cost = cost;
            _isAsphalt = isAsphalt;

            _mesh = mesh;
            _texture = texture;
            _material = material;

            _propBlock = new MaterialPropertyBlock();
        }

        public Mesh GetMesh() => _mesh;
        public Material GetMaterial() => _material;
        public Texture GetTexture() => _texture;
        public int GetCost() => _cost;
        public bool IsAsphalt() => _isAsphalt;
        public MaterialPropertyBlock SetTextureFromPropertyBlock(Renderer renderer)
        {
            renderer.GetPropertyBlock(_propBlock);

            _propBlock.SetTexture("_BaseMap", _texture);

            return _propBlock;
        }

    }

}
