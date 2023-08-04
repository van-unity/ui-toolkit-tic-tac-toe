using UnityEngine;

namespace TicTacToe.Editor.Domain {
    public interface IGameSettings {
        Vector2 WindowDimensions { get; }
        int BoardSize { get; }
        PlayerMode PlayerXMode { get; }
        PlayerMode PlayerOMode { get; }
        int BoardDrawDelayMS { get; }
        int AutomatedPlayerDelayMS { get; }

        void SetPlayerXMode(PlayerMode playerMode);
        void SetPlayerOMode(PlayerMode playerMode);
    }
}