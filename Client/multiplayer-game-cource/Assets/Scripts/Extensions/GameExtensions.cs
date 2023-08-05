using Assets.Scripts.DataStructs;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class GameExtensions
    {
        public static Vector3 ToPosition(this ShootInfo source) => new(source.pX, source.pY, source.pZ);
        
        public static Vector3 ToDirection(this ShootInfo source) => new(source.dX, source.dY, source.dZ);

        public static void SetScale(this Transform source, float value) => source.localScale = new Vector3(value, value, value);
        
        public static void SetScale(this GameObject source, float value) => source.transform.localScale = new Vector3(value, value, value);
    }
}