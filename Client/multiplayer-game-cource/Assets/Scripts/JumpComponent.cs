using UnityEngine;

namespace Assets.Scripts
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private FlyInfo _flyInfo;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _jumpThreshold = 0.2f;
        [SerializeField] private ForceMode _forceMode = ForceMode.VelocityChange;

        private float _lastJumpTime;

        public void Jump()
        {
            if (_flyInfo.IsFly) return;
            if (Time.time - _lastJumpTime < _jumpThreshold) return;

            _rb.AddForce(0, _jumpForce, 0, _forceMode);
            _lastJumpTime = Time.time;
        }
    }
}