using UnityEngine;

namespace Assets.Scripts
{
    public class VelocityInfo : MonoBehaviour
    {
        [SerializeField] private Character _character;

        public Vector3 LocalVelocity => GetLocalVelocity();
        public float NormalizedSpeed => GetNormalizedSpeed();

        #region Methods

        private Vector3 GetLocalVelocity()
        {
            return _character.transform.InverseTransformVector(_character.Velocity);
        }

        private float GetNormalizedSpeed()
        {
            var velocity = GetLocalVelocity();
            var speed = velocity.magnitude / _character.Speed;
            var sign = Mathf.Sign(velocity.z);

            return speed * sign;
        }

        #endregion
    }
}