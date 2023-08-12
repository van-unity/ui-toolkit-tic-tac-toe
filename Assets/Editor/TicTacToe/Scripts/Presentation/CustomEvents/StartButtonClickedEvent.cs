using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation.CustomEvents {
    public class StartButtonClickEvent : EventBase<StartButtonClickEvent> {
        public StartButtonClickEvent(IEventHandler target) {
            this.target = target;
        }

        public StartButtonClickEvent() {
        }
    }
}