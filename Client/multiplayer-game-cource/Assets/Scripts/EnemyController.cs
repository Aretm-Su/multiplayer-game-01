using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyCharacter _enemy;

        public void OnChange(List<DataChange> changes)
        {
            Vector3 position = transform.position;
            
            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case "x": position.x = (float) dataChange.Value;
                        break;
                    case "y": position.z = (float) dataChange.Value;
                        break;
                    
                    default: Debug.LogWarning($"Не обрабатывается изменение поля {dataChange.Field}!");
                        break;
                }
            }

            transform.position = position;
        }
    }
}