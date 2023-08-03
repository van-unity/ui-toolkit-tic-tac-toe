using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation.CustomEvents {
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