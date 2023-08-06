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
        private float _lastReceiveTime;
        private Player _player;

        private float AverageInterval => _receiveTimeIntervals.Sum() / _receiveTimeIntervals.Count;

        public void Init(Player player)
        {
            player.OnChange += OnChange;

            _player = player;
            _character.SetSpeed(player.speed);
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

        private void SaveReceiveTime()
        {
            float interval = Time.time - _lastReceiveTime;

            _lastReceiveTime = Time.time;
            _receiveTimeIntervals.Add(interval);
            _receiveTimeIntervals.Remove(0);
        }

        private void OnChange(List<DataChange> changes)
        {
            SaveReceiveTime();

            Vector3 position = transform.position;
            Vector3 velocity = _character.Velocity;
            Vector3 angularVelocity = _character.AngularVelocity;
            Vector3 headRotation = Vector3.zero;
            Vector3 bodyRotation = Vector3.zero;
            bool squatState = false;

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
                        headRotation.x = (float) dataChange.Value;
                        break;
                    case ServerKeys.RotationY:
                        bodyRotation.y = (float) dataChange.Value;
                        break;
                    case ServerKeys.AngularVelocityY:
                        angularVelocity.y = (float) dataChange.Value;
                        break;
                    case ServerKeys.SquattingState:
                        squatState = (bool) dataChange.Value;
                        break;

                    default:
                        Debug.LogWarning($"Не обрабатывается изменение поля {dataChange.Field}!");
                        break;
                }
            }

            _character.SetMovement(position, velocity, AverageInterval);
            _character.SetBodyRotation(bodyRotation, angularVelocity, AverageInterval);
            _character.SetHeadRotation(headRotation);
            _character.Squat(squatState);
        }
    }
}