using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation.CustomEvents {
    public class StartButtonClickEvent : EventBase<StartButtonClickEvent> {
        public StartButtonClickEvent(IEventHandler target) {
            this.target = target;
        }

        public StartButtonClickEvent() {
        }
    }
}