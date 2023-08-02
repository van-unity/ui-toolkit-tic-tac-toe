namespace TicTacToe.Editor.Domain {
    public interface IGameController {
        bool IsGameStarted { get; }
        void Start();
        void Restart();
        void TogglePlayerMode(Symbol playerSymbol);
    }
}