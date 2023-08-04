namespace TicTacToe.Editor.Domain {
    public interface IGameController {
        void Start();
        void Restart();
        void TogglePlayerMode(Symbol playerSymbol);
    }
}