using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Bullet _bulletPrefab;

        public event Action OnShoot = delegate {  };

        protected void InvokeShootAction() => OnShoot();
    }
}