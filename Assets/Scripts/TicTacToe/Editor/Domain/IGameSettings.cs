namespace TicTacToe.Editor.Domain {
    public interface IGameSettings {
        int BoardSize { get; }
        PlayerMode PlayerXMode { get; }
        PlayerMode PlayerOMode { get; }
        int BoardDrawDelayMS { get; }

        void SetPlayerXMode(PlayerMode playerMode);
        void SetPlayerOMode(PlayerMode playerMode);
    }
}