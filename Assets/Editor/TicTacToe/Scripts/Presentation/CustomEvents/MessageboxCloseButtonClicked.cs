using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation.CustomEvents {
    public class MessageboxCloseButtonClicked : EventBase<MessageboxCloseButtonClicked> {
        public MessageboxCloseButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public MessageboxCloseButtonClicked() {
        }
    }
}