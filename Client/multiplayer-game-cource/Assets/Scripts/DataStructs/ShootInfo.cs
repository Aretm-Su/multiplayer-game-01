using System;
using UnityEngine;

namespace Assets.Scripts.DataStructs
{
    [Serializable]
    public struct ShootInfo
    {
        public ShootInfo(Vector3 startPosition, Vector3 resultVelocity)
        {
            key = string.Empty;
            pX = startPosition.x;
            pY = startPosition.y;
            pZ = startPosition.z;
            dX = resultVelocity.x;
            dY = resultVelocity.y;
            dZ = resultVelocity.z;
        }
        
        public string key;
        public float pX;
        public float pY;
        public float pZ;
        public float dX;
        public float dY;
        public float dZ;
    }
}