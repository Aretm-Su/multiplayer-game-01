using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private SitComponent _sit;
        
        [field: SerializeField] public float Speed { get; protected set; } = 6.5f;
        public Vector3 Velocity { get; protected set; }

        public bool TryGetUp() => _sit.TryGetUp();

        public bool TryGetDown() => _sit.TryGetDown();
    }
}