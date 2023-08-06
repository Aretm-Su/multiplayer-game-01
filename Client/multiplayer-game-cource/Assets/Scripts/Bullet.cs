using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _lifeTime = 10f;
        
        public void Init(Vector3 velocity)
        {
            _rb.velocity = velocity;

            StartCoroutine(LifetimeRoutine());
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy();
        }

        private IEnumerator LifetimeRoutine()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            
            Destroy();
        }

        private void Destroy() => Destroy(gameObject);
    }
}