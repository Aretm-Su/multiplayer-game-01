using UnityEngine;

namespace Assets.Scripts
{
    public class FlyInfo : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _lastChanceDuration;

        private float _flyTime = 0;

        public bool IsFly { get; private set; }
        public bool IsGrounded => !IsFly;

        private void FixedUpdate()
        {
            if (Physics.CheckSphere(transform.position, _radius, _layerMask))
            {
                IsFly = false;
                _flyTime = 0;
            }
            else
            {
                _flyTime += Time.fixedDeltaTime;
                IsFly = _flyTime> _lastChanceDuration;
            }
        }

        #region Editor

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
        
        #endregion
    }
}