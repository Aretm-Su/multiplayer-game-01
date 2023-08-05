using Assets.Scripts.DataStructs;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerGun : Gun
    {
        [SerializeField] private Transform _bulletPoint;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _shootThreshold;

        private float _lastShootTime;

        public bool TryShoot(out ShootInfo shootInfo)
        {
            if (Time.time - _lastShootTime < _shootThreshold)
            {
                shootInfo = default;
                return false;
            }

            var position = _bulletPoint.position;
            var direction = _bulletPoint.forward;
            var resultDirection = direction * _bulletSpeed;
            
            _lastShootTime = Time.time;
            
            Instantiate(_bulletPrefab, position, _bulletPoint.rotation).Init(resultDirection);
            InvokeShootAction();

            shootInfo = new ShootInfo(position, resultDirection);

            return true;
        }
    }
}