using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; protected set; } = 6.5f;
        public Vector3 Velocity { get; protected set; }
        public Vector3 AngularVelocity { get; protected set; }
    }
}