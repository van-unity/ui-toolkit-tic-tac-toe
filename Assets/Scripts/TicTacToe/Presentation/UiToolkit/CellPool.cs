using System.Collections.Generic;

namespace TicTacToe.Presentation.UiToolkit {
    public class CellPool {
        private readonly IStyleSettings _styleSettings;
        private readonly Stack<Cell> _pool;

        public CellPool(int initialSize, IStyleSettings styleSettings) {
            _styleSettings = styleSettings;
            _pool = new Stack<Cell>(initialSize);
            for (int i = 0; i < initialSize; i++) {
                _pool.Push(CreateNewCell());
            }
        }

        public Cell GetCell() {
            return _pool.Count == 0 ? CreateNewCell() : _pool.Pop();
        }

        public void ReturnCell(Cell cell) {
            _pool.Push(cell);
        }

        private Cell CreateNewCell() => new(_styleSettings);
    }
}