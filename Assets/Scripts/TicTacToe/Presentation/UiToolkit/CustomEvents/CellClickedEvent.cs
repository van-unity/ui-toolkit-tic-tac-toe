using TicTacToe.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.CustomEvents {
    public class CellClickedEvent : EventBase<CellClickedEvent> {
        public BoardPosition ClickPosition { get; }

        public CellClickedEvent(BoardPosition clickPosition, IEventHandler target) {
            ClickPosition = clickPosition;
            this.target = target;
        }

        public CellClickedEvent() : this(BoardPosition.Invalid, null) {
        }
    }
}