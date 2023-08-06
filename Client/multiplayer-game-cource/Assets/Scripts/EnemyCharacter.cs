using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyCharacter : Character
    {
        [SerializeField] private Transform _head;
        [SerializeField] private SquatComponent _squat;

        private Vector3 _targetPosition;
        private Vector3 _bodyRotation;
        private Vector3 _headRotation;
        private float _velocityMagnitude;
        private float _angularVelocityMagnitude;

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

        public void SetMovement(in Vector3 serverPosition, in Vector3 serverVelocity, in float averageInterval)
        {
            _targetPosition = serverPosition + (serverVelocity * averageInterval);
            _velocityMagnitude = serverVelocity.magnitude;

            Velocity = serverVelocity;
        }

        public void SetBodyRotation(in Vector3 serverRotation, in Vector3 serverAngularVelocity, in float averageInterval)
        {
            _bodyRotation = serverRotation + (serverAngularVelocity * averageInterval);
            _angularVelocityMagnitude = serverAngularVelocity.magnitude;
            
            AngularVelocity = serverAngularVelocity;
        }

        public void SetHeadRotation(in Vector3 serverRotation)
        {
            _headRotation = serverRotation;
        }

        public void Squat(bool value)
        {
            _squat.SetSquatState(value);
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
            _head.localEulerAngles = _headRotation;
        }

        private void RefreshBodyPosition()
        {
            if (_angularVelocityMagnitude > 0.1f)
            {
                transform.localEulerAngles = _bodyRotation;
            }
        }
        
        #endregion
    }
}