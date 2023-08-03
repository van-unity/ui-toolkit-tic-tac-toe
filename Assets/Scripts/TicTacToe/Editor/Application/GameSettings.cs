using TicTacToe.Editor.Domain;
using UnityEngine;

namespace TicTacToe.Editor.Application {
    [CreateAssetMenu(menuName = "TicTacToe/GameSettings", fileName = "GameSettings")]
    public sealed class GameSettings : ScriptableObject, IGameSettings {
        [SerializeField] private Vector2 _boardDimensions = new(640, 960);
        [SerializeField] private int _boardSize = 3;
        [SerializeField] private PlayerMode _playerXMode = PlayerMode.Manual;
        [SerializeField] private PlayerMode _playerOMode = PlayerMode.Auto;
        [SerializeField] private int _boardDrawDelayMS = 1000;

        public Vector2 BoardDimensions => _boardDimensions;
        public int BoardSize => _boardSize;
        public PlayerMode PlayerXMode => _playerXMode;
        public PlayerMode PlayerOMode => _playerOMode;
        public int BoardDrawDelayMS => _boardDrawDelayMS;

        public void SetPlayerXMode(PlayerMode playerMode) {
            _playerXMode = playerMode;
        }

        public void SetPlayerOMode(PlayerMode playerMode) {
            _playerOMode = playerMode;
        }
    }
}