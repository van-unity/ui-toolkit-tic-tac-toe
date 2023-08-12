using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.CustomEvents {
    public class StartButtonClickEvent : EventBase<StartButtonClickEvent> {
        public StartButtonClickEvent(IEventHandler target) {
            this.target = target;
        }

        public StartButtonClickEvent() {
        }
    }
}