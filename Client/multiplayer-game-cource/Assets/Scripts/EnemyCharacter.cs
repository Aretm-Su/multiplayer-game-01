using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyCharacter : Character
    {
        [SerializeField] private Transform _head;

        private Vector3 _targetPosition = Vector3.zero;
        private float _velocityMagnitude;
        private float _rotationX;
        private float _rotationY;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            RefreshPosition();
            RefreshHeadRotation();
            RefreshBodyPosition();
        }

        public void SetSpeed(float value)
        {
            Speed = value;
        }

        public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
        {
            _targetPosition = position + (velocity * averageInterval);
            _velocityMagnitude = velocity.magnitude;

            Velocity = velocity;
        }

        public void SetRotateX(float value)
        {
            _rotationX = value;
        }

        public void SetRotateY(float value)
        {
            _rotationY = value;
        }

        #region Methods

        private void RefreshPosition()
        {
            if (_velocityMagnitude > 0.1f)
            {
                var maxDistance = _velocityMagnitude * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, maxDistance);
            }
            else
            {
                transform.position = _targetPosition;
            }
        }

        private void RefreshHeadRotation()
        {
            _head.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }

        private void RefreshBodyPosition()
        {
            transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        }
        
        #endregion
    }
}