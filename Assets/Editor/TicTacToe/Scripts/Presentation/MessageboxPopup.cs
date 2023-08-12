using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions;
using Editor.TicTacToe.Scripts.Utils;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class MessageboxPopup : VisualElement, IPopup {
        private const int ANIMATION_DURATION_MS = 200;
        
        private readonly Label _messageLabel;
        private readonly VisualElement _messagebox;
        private readonly VisualElement _backdrop;

        public MessageboxPopup(IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.MessageboxPopupStyle);
            this.AddToClassList("popup");

            _backdrop = new VisualElement();
            _backdrop.AddToClassList("backdrop");
            _backdrop.AddToClassList("backdrop-hidden");
            _backdrop.RegisterCallback<ClickEvent>(_ => OnCloseButtonClick());
            _messageLabel = new Label();
            _messageLabel.AddToClassList("message-label");

            _messagebox = new VisualElement();
            _messagebox.AddToClassList("message-box");
            _messagebox.AddToClassList("message-box-hidden");
            _messagebox.Add(_messageLabel);

            this.Add(_backdrop);
            this.Add(_messagebox);
        }

        private void OnCloseButtonClick() {
            var clickedEvent = new MessageboxCloseButtonClicked(this);
            this.SendEvent(clickedEvent);
        }

        public void SetMessage(string message) {
            _messageLabel.text = message;
        }

        public async Task ShowAsync() {
            await Task.Delay(TimeSettings.DELTA_TIME_MS); // delay one frame
            _messagebox.RemoveFromClassList("message-box-hidden");
            _messagebox.AddToClassList("message-box-shown");
            _backdrop.RemoveFromClassList("backdrop-hidden");
            _backdrop.AddToClassList("backdrop-shown");
            await Task.Delay(ANIMATION_DURATION_MS); //duration of animation
        }

        public async Task HideAsync() {
            _messagebox.RemoveFromClassList("message-box-shown");
            _messagebox.AddToClassList("message-box-hidden");
            _backdrop.RemoveFromClassList("backdrop-shown");
            _backdrop.AddToClassList("backdrop-hidden");
            await Task.Delay(ANIMATION_DURATION_MS); //duration of animation
        }
    }
}