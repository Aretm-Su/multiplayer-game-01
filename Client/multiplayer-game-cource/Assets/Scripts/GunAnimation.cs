using UnityEngine;

namespace Assets.Scripts
{
    public class GunAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Gun _gun;

        private readonly int Shoot = Animator.StringToHash("Shoot");

        private void Start()
        {
            _gun.OnShoot += ShootHandler;
        }

        private void OnDestroy()
        {
            _gun.OnShoot -= ShootHandler;
        }

        private void ShootHandler()
        {
            animator.SetTrigger(Shoot);
        }
    }
}