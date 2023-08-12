namespace TicTacToe.Domain {
    /// <summary>
    /// Defines the settings for a TicTacToe game.
    /// </summary>
    public interface IGameSettings {
        /// <summary>
        /// Gets the size of the game board (e.g., 3 for a 3x3 board).
        /// </summary>
        int BoardSize { get; }

        /// <summary>
        /// Gets the mode for Player X (e.g., Manual or Automated).
        /// </summary>
        PlayerMode PlayerXMode { get; }

        /// <summary>
        /// Gets the mode for Player O (e.g., Manual or Automated).
        /// </summary>
        PlayerMode PlayerOMode { get; }

        /// <summary>
        /// Gets the delay in milliseconds for automated player moves.
        /// </summary>
        int AutomatedPlayerDelayMS { get; }
    }
}