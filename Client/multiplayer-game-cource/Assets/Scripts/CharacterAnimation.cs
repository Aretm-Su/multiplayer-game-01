using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private FlyInfo _flyInfo;
        [SerializeField] private VelocityInfo _velocityInfo;
        [SerializeField] private Animator _animator;

        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            _animator.SetBool(Grounded, _flyInfo.IsGrounded);
            _animator.SetFloat(Speed, _velocityInfo.NormalizedSpeed);
        }
    }
}