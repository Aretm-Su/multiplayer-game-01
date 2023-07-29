using System.Collections.Generic;
using Colyseus;
using Colyseus.Schema;
using UnityEngine;

namespace DefaultNamespace.Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private EnemyController _enemy;
        
        private ColyseusRoom<State> _room;

        protected override void Awake()
        {
            base.Awake();
            
            Instance.InitializeClient();
            Connect();
        }

        private async void Connect()
        {
            _room = await Instance.client.JoinOrCreate<State>("state_handler");
            _room.OnStateChange += StateChangeListener;
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

        private void StateChangeListener(State state, bool isFirstState)
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

        private void CreatePlayer(State state)
        {
            var player = state.players[_room.SessionId];
            var position = new Vector3(player.x, 0f, player.y);

            Instantiate(_player, position, Quaternion.identity);
        }

        private void CreateEnemy(string key, Player player)
        {
            var position = new Vector3(player.x, 0f, player.y);
            var enemy = Instantiate(_enemy, position, Quaternion.identity);
            
            player.OnChange += enemy.OnChange;
        }

        private void RemoveEnemy(string key, Player player)
        {
            
        }
    }
}