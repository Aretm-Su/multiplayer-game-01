using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DataStructs;
using Assets.Scripts.Extensions;
using Assets.Scripts.Multiplayer.Generated;
using Assets.Scripts.StaticData;
using Colyseus.Schema;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyCharacter _character;
        [SerializeField] private EnemyGun _gun;

        private readonly List<float> _receiveTimeIntervals = new(5) {0, 0, 0, 0, 0};
        private float _lastReceiveTime = 0;
        private Player _player;

        private float AverageInterval => _receiveTimeIntervals.Sum() / _receiveTimeIntervals.Count;

        public void Init(Player player)
        {
            _player = player;
            _character.SetSpeed(player.speed);
            player.OnChange += OnChange;
        }

        public void Dispose()
        {
            _player.OnChange -= OnChange;
            Destroy(gameObject);
        }

        public void Shoot(in ShootInfo info)
        {
            _gun.Shoot(info.ToPosition(), info.ToDirection());
        }

        public void GetDown()
        {
            _character.TryGetDown();
        }

        public void GetUp()
        {
            _character.TryGetUp();
        }

        private void SaveReceiveTime()
        {
            float interval = Time.time - _lastReceiveTime;

            _lastReceiveTime = Time.time;
            _receiveTimeIntervals.Add(interval);
            _receiveTimeIntervals.Remove(0);
        }

        public void OnChange(List<DataChange> changes)
        {
            SaveReceiveTime();

            Vector3 position = transform.position;
            Vector3 velocity = _character.Velocity;

            foreach (var dataChange in changes)
            {
                switch (dataChange.Field)
                {
                    case ServerKeys.PositionX:
                        position.x = (float) dataChange.Value;
                        break;
                    case ServerKeys.PositionY:
                        position.y = (float) dataChange.Value;
                        break;
                    case ServerKeys.PositionZ:
                        position.z = (float) dataChange.Value;
                        break;
                    case ServerKeys.VelocityX:
                        velocity.x = (float) dataChange.Value;
                        break;
                    case ServerKeys.VelocityY:
                        velocity.y = (float) dataChange.Value;
                        break;
                    case ServerKeys.VelocityZ:
                        velocity.z = (float) dataChange.Value;
                        break;
                    case ServerKeys.RotationX:
                        _character.SetRotateX((float)dataChange.Value);
                        break;
                    case ServerKeys.RotationY:
                        _character.SetRotateY((float) dataChange.Value);
                        break;

                    default:
                        Debug.LogWarning($"Не обрабатывается изменение поля {dataChange.Field}!");
                        break;
                }
            }

            _character.SetMovement(position, velocity, AverageInterval);
        }
    }
}