namespace Editor.TicTacToe.Scripts.Domain {
    /// <summary>
    /// Exposes public methods for Starting, Restarting the game and Toggling player modes.
    /// </summary>
    public interface IGameController {
        void Start();
        
        void Restart();
        
        void TogglePlayerMode(PlayerSymbol playerSymbol);
    }
}