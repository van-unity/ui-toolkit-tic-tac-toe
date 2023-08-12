using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation.CustomEvents {
    public class RestartButtonClicked : EventBase<RestartButtonClicked> {
        public RestartButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public RestartButtonClicked() {
        }
    }
}