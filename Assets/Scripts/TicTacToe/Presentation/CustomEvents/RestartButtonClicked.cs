using UnityEngine.UIElements;

namespace TicTacToe.Presentation.CustomEvents {
    public class RestartButtonClicked : EventBase<RestartButtonClicked> {
        public RestartButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public RestartButtonClicked() {
        }
    }
}