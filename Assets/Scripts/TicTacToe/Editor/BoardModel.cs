namespace TicTacToe.Editor {
    public class BoardModel {
        private readonly PlayerSymbol[,] _state;

        public int Rows { get; }
        public int Columns { get; }

        public BoardModel(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            _state = new PlayerSymbol[Rows, Columns];
        }

        public void PlaceSymbolAt(PlayerSymbol symbol, BoardPosition position) {
            _state[position.rowIndex, position.columnIndex] = symbol;
        }

        public PlayerSymbol ValueAt(BoardPosition position) => _state[position.rowIndex, position.columnIndex];

        public bool IsBoardFull() {
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++) {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == PlayerSymbol.None) {
                        // Found an empty spot, so the board is not full
                        return false;
                    }
                }
            }

            // No empty spots were found, so the board is full
            return true;
        }

        public bool HasWinner(BoardPosition lastMovePos, out Win win) {
            PlayerSymbol lastMoveSymbol = _state[lastMovePos.rowIndex, lastMovePos.columnIndex];

            // Check the row of the last move
            var rowIsUniform = true;
            for (int i = 0; i < Rows; i++) {
                if (_state[lastMovePos.rowIndex, i] == lastMoveSymbol) {
                    continue;
                }

                rowIsUniform = false;
                break;
            }

            if (rowIsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(lastMovePos.rowIndex, 0),
                    new BoardPosition(lastMovePos.rowIndex, Rows - 1));
                return true;
            }

            // Check the column of the last move
            var colIsUniform = true;
            for (int i = 0; i < Rows; i++) {
                if (_state[i, lastMovePos.columnIndex] == lastMoveSymbol) {
                    continue;
                }

                colIsUniform = false;
                break;
            }

            if (colIsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, lastMovePos.columnIndex),
                    new BoardPosition(Rows - 1, lastMovePos.columnIndex));
                return true;
            }

            // Check the diagonals if the last move was on a diagonal
            var onDiagonal1 = lastMovePos.rowIndex == lastMovePos.columnIndex;
            var onDiagonal2 = lastMovePos.rowIndex == Rows - 1 - lastMovePos.columnIndex;
            var diag1IsUniform = true;
            var diag2IsUniform = true;
            for (int i = 0; i < Rows; i++) {
                if (onDiagonal1 && _state[i, i] != lastMoveSymbol)
                    diag1IsUniform = false;
                if (onDiagonal2 && _state[i, Rows - 1 - i] != lastMoveSymbol)
                    diag2IsUniform = false;
            }

            if (onDiagonal1 && diag1IsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, 0), new BoardPosition(Rows - 1, Rows - 1));
                return true;
            }

            if (onDiagonal2 && diag2IsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, Rows - 1), new BoardPosition(Rows - 1, 0));
                return true;
            }

            // No winner
            win = null;
            return false;
        }
    }
}