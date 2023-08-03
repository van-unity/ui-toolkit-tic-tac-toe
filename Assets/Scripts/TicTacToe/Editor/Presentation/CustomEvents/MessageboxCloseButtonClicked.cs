using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation.CustomEvents {
    public class MessageboxCloseButtonClicked : EventBase<MessageboxCloseButtonClicked> {
        public MessageboxCloseButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public MessageboxCloseButtonClicked() {
        }
    }
}