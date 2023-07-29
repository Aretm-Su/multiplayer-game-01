using System.Collections.Generic;
using DefaultNamespace.Multiplayer;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _player;

        private float _inputH;
        private float _inputV;

        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private void Update()
        {
            float h = Input.GetAxisRaw(HorizontalAxis);
            float v = Input.GetAxisRaw(VerticalAxis);

            _player.SetInput(h, v);
            
            SendMove();
        }

        private void SendMove()
        {
            _player.GetMoveInfo(out Vector3 position);
            Dictionary<string, object> data = new()
            {
                {"x", position.x},
                {"y", position.z},
            };
            MultiplayerManager.Instance.SendMessage("move", data);
        }
    }
}