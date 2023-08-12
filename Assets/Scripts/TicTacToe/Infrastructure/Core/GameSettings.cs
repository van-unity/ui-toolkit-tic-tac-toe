using TicTacToe.Domain;
using TicTacToe.Utils;
using UnityEngine;

namespace TicTacToe.Infrastructure.Core {
    /// <summary>
    /// Represents the persistent game settings for TicTacToe as a ScriptableObject.
    /// </summary>
    [CreateAssetMenu(menuName = MenuNames.SETTINGS + "/GameSettings", fileName = "GameSettings")]
    public sealed class GameSettings : ScriptableObject, IGameSettings {
        [SerializeField] private int _boardSize = 3;
        [SerializeField] private PlayerMode _playerXMode = PlayerMode.Manual;
        [SerializeField] private PlayerMode _playerOMode = PlayerMode.Auto;
        [SerializeField] private int _automatedPlayerDelayMS = 1000;

        public int BoardSize => _boardSize;
        
        public PlayerMode PlayerXMode => _playerXMode;
        
        public PlayerMode PlayerOMode => _playerOMode;
        
        public int AutomatedPlayerDelayMS => _automatedPlayerDelayMS;
    }
}