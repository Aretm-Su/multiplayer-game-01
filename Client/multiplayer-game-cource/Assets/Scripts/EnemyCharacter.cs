using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyCharacter: MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}