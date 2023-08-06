using System;
using System.Collections.Generic;
using Assets.Scripts.DataStructs;
using Assets.Scripts.Multiplayer;
using Assets.Scripts.StaticData;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private PlayerGun _gun;
        [SerializeField] private float mouseSensitivity = 2f;

        private MultiplayerManager _multiplayerManager;
        private float _inputH;
        private float _inputV;

        private void Start()
        {
            _multiplayerManager = MultiplayerManager.Instance;
        }

        private void Update()
        {
            float h = Input.GetAxisRaw(InputKeys.HorizontalAxis);
            float v = Input.GetAxisRaw(InputKeys.VerticalAxis);

            float mouseX = Input.GetAxis(InputKeys.MouseX) * mouseSensitivity;
            float mouseY = Input.GetAxis(InputKeys.MouseY) * mouseSensitivity;

            bool space = Input.GetKeyDown(KeyCode.Space);
            bool isSquatting = Input.GetKey(KeyCode.C);
            bool isShoot = Input.GetMouseButton(0);

            _player.SetInput(h, v, mouseX);
            _player.RotateHead(-mouseY);
            _player.Squat(isSquatting);

            if (space)
            {
                _player.Jump();
            }

            if (isShoot && _gun.TryShoot(out ShootInfo info))
            {
                SendShoot(ref info);
            }

            SendPlayerState();
        }

        private void SendShoot(ref ShootInfo info)
        {
            info.key = _multiplayerManager.GetClientKey();
            string json = JsonUtility.ToJson(info);
            
            _multiplayerManager.SendMessage(ServerKeys.ShootMessage, json);
        }

        private void SendPlayerState()
        {
            _player.GetMoveInfo(out Vector3 position, out Vector3 velocity);
            _player.GetRotationInfo(out Vector3 angularVelocity, out Vector3 headRotation, out Vector3 bodyRotation);
            _player.GetCharacterInfo(out bool squatState);
            
            Dictionary<string, object> data = new()
            {
                {ServerKeys.PositionX, position.x},
                {ServerKeys.PositionY, position.y},
                {ServerKeys.PositionZ, position.z},
                {ServerKeys.VelocityX, velocity.x},
                {ServerKeys.VelocityY, velocity.y},
                {ServerKeys.VelocityZ, velocity.z},
                {ServerKeys.RotationX, headRotation.x},
                {ServerKeys.RotationY, bodyRotation.y},
                {ServerKeys.AngularVelocityY, angularVelocity.y},
                {ServerKeys.SquattingState, squatState},
            };
            
            _multiplayerManager.SendMessage(ServerKeys.MoveMessage, data);
        }
    }
}