using UnityEngine.UIElements;

namespace TicTacToe.Presentation.CustomEvents {
    public class MessageboxCloseButtonClicked : EventBase<MessageboxCloseButtonClicked> {
        public MessageboxCloseButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public MessageboxCloseButtonClicked() {
        }
    }
}