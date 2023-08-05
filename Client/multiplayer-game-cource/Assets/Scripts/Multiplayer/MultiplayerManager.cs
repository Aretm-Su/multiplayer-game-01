using System.Collections.Generic;
using Assets.Scripts.DataStructs;
using Assets.Scripts.Extensions;
using Assets.Scripts.Multiplayer.Generated;
using Assets.Scripts.StaticData;
using Colyseus;
using UnityEngine;

namespace Assets.Scripts.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private EnemyController _enemy;
        
        private ColyseusRoom<State> _room;
        private readonly Dictionary<string, EnemyController> _enemies = new();

        protected override void Awake()
        {
            base.Awake();
            
            Instance.InitializeClient();
            Connect();
        }

        private async void Connect()
        {
            Dictionary<string, object> data = new()
            {
                {"speed", _player.Speed}
            };

            _room = await Instance.client.JoinOrCreate<State>("state_handler", data);
            _room.OnStateChange += StateChangeHandler;
            _room.OnMessage<string>(ServerKeys.ShootMessageFromServer, ShootMessageHandler);
            _room.OnMessage<string>(ServerKeys.GetDownMessageFromServer, GetDownMessageHandler);
            _room.OnMessage<string>(ServerKeys.GetUpMessageFromServer, GetUpMessageHandler);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _room.Leave();
        }

        public void SendMessage(string key, Dictionary<string, object> data)
        {
            _room.Send(key, data);
        }

        public void SendMessage(string key, string data)
        {
            _room.Send(key, data);
        }

        public string GetClientKey()
        {
            return _room.SessionId;
        }

        private void StateChangeHandler(State state, bool isFirstState)
        {
            if (isFirstState == false) return;

            state.players.ForEach((key, player) =>
            {
                if (key == _room.SessionId) CreatePlayer(state);
                
                else CreateEnemy(key, player);
            });

            state.players.OnAdd += CreateEnemy;
            state.players.OnRemove += RemoveEnemy;
        }

        private void ShootMessageHandler(string data)
        {
            ShootInfo info = JsonUtility.FromJson<ShootInfo>(data);

            if (_enemies.HasNotKey(info.key))
            {
                Debug.LogError($"Нет Enemy с ключем {info.key} ");
            }
            
            _enemies[info.key].Shoot(info);
        }
        
        private void GetDownMessageHandler(string data)
        {
            ClientInfo info = JsonUtility.FromJson<ClientInfo>(data);
            
            if (_enemies.HasNotKey(info.key))
            {
                Debug.LogError($"Нет Enemy с ключем {info.key} ");
            }
            
            _enemies[info.key].GetDown();
        }

        private void GetUpMessageHandler(string data)
        {
            ClientInfo info = JsonUtility.FromJson<ClientInfo>(data);
            
            if (_enemies.HasNotKey(info.key))
            {
                Debug.LogError($"Нет Enemy с ключем {info.key} ");
            }
            
            _enemies[info.key].GetUp();
        }

        private void CreatePlayer(State state)
        {
            var player = state.players[_room.SessionId];
            var position = new Vector3(player.pX, player.pY, player.pZ);
            
            Instantiate(_player, position, Quaternion.identity);
        }

        private void CreateEnemy(string key, Player player)
        {
            var position = new Vector3(player.pX, player.pY, player.pZ);
            var enemy = Instantiate(_enemy, position, Quaternion.identity);
            
            enemy.Init(player);
            _enemies.Add(key, enemy);
        }

        private void RemoveEnemy(string key, Player player)
        {
            if (_enemies.ContainsKey(key))
            {
                _enemies[key].Dispose();
                _enemies.Remove(key);
            }
        }
    }
}