using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation.CustomEvents {
    public class PlayerModeClickedEvent : EventBase<PlayerModeClickedEvent> {
        public Symbol Symbol { get; }

        public PlayerModeClickedEvent(Symbol symbol, IEventHandler target) {
            Symbol = symbol;
            this.target = target;
        }

        public PlayerModeClickedEvent() {
            Symbol = Symbol.Empty;
        }
    }
}