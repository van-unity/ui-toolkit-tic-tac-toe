using System;
using System.Threading.Tasks;
using TicTacToe.Presentation.VisualElementExtensions;
using TicTacToe.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation {
    public class MessageboxPopup : VisualElement, IPopup {
        private const int ANIMATION_DURATION_MS = 200;

        private readonly Label _messageLabel;
        private readonly VisualElement _messagebox;
        private readonly VisualElement _backdrop;

        public event Action Opened;
        public event Action Closed;
        public event Action OkButtonClicked;

        public MessageboxPopup(StyleSheet style) {
            this.SetStyle(style);
            this.AddToClassList("popup");

            _backdrop = new VisualElement();
            _backdrop.AddToClassList("backdrop");
            _backdrop.AddToClassList("backdrop-hidden");
            _messageLabel = new Label();
            _messageLabel.AddToClassList("message-label");

            _messagebox = new VisualElement();
            _messagebox.AddToClassList("message-box");
            _messagebox.AddToClassList("message-box-hidden");
            _messagebox.Add(_messageLabel);

            this.Add(_backdrop);
            this.Add(_messagebox);

            this.RegisterCallback<AttachToPanelEvent>(_ => Opened?.Invoke());
            this.RegisterCallback<DetachFromPanelEvent>(_ => Closed?.Invoke());
            _backdrop.RegisterCallback<ClickEvent>(_ => OkButtonClicked?.Invoke());
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