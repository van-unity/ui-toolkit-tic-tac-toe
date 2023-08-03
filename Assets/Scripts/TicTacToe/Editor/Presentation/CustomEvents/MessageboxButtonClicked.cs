using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation.CustomEvents {
    public class MessageboxButtonClicked : EventBase<MessageboxButtonClicked> {
        public MessageboxButtonClicked(IEventHandler target) {
            this.target = target;
        }

        public MessageboxButtonClicked() {
        }
    }
}