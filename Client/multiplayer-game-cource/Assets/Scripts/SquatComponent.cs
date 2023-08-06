using UnityEngine;

namespace Assets.Scripts
{
    public class SquatComponent : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly int Squatting = Animator.StringToHash("Squatting");

        public bool IsSquatting => animator.GetBool(Squatting);

        public void SetSquatState(bool value)
        {
            if (value) TryGetDown();
            
            else TryGetUp();
        }

        private void TryGetDown()
        {
            if (IsSquatting) return;
            
            animator.SetBool(Squatting, true);
        }

        private void TryGetUp()
        {
            if (!IsSquatting) return;
            
            animator.SetBool(Squatting, false);
        }
    }
}