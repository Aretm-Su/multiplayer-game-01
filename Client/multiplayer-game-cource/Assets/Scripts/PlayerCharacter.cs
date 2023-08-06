using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCharacter : Character
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private JumpComponent _jumper;
        [SerializeField] private SquatComponent _squat;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private float _minHeadAngleX = -90f;
        [SerializeField] private float _maxHeadAngleX = 90f;

        private float _inputH;
        private float _inputV;
        private float _rotateY;
        private float _currentHeadRotateX;

        private void Start()
        {
            Transform camera = Camera.main.transform;

            camera.parent = _cameraPoint;
            camera.localPosition = Vector3.zero;
            camera.localRotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            Move();
            RotateBody();
        }

        public void SetInput(float h, float v, float mouseX)
        {
            _inputH = h;
            _inputV = v;
            _rotateY += mouseX;
        }

        public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
        {
            position = transform.position;
            velocity = _rb.velocity;
        }

        public void GetRotationInfo(out Vector3 angularVelocity, out Vector3 headRotation, out Vector3 bodyRotation)
        {
            angularVelocity = _rb.angularVelocity;
            headRotation = new Vector3(_head.localEulerAngles.x, 0, 0);
            bodyRotation = new Vector3(0, transform.localEulerAngles.y, 0);
        }

        public void GetCharacterInfo(out bool squatState)
        {
            squatState = _squat.IsSquatting;
        }

        public void RotateHead(float value)
        {
            _currentHeadRotateX = Mathf.Clamp(_currentHeadRotateX + value, _minHeadAngleX, _maxHeadAngleX);
            _head.localEulerAngles = new Vector3(_currentHeadRotateX, 0, 0);
        }

        public void Jump()
        {
            _jumper.Jump();
        }

        public void Squat(bool state)
        {
            _squat.SetSquatState(state);
        }

        #region Methods

        private void Move()
        {
            Vector3 velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * Speed;

            velocity.y = _rb.velocity.y;
            _rb.velocity = velocity;
            
            Velocity = velocity;
        }

        private void RotateBody()
        {
            _rb.angularVelocity = new Vector3(0, _rotateY, 0);
            _rotateY = 0;

            AngularVelocity = _rb.angularVelocity;
        }
        
        #endregion
    }
}