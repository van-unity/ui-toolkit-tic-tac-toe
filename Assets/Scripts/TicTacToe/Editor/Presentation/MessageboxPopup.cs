using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation.CustomEvents;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class MessageboxPopup : VisualElement, IPopup {
        private readonly Label _messageLabel;
        private readonly VisualElement _messagebox;
        private readonly Button _button;
        
        public MessageboxPopup(IStyleSettings styleSettings) {
            this.SetStyleFromPath(styleSettings.MessageboxPopupStyle);
            this.AddToClassList("popup");

            var backdrop = new VisualElement();
            backdrop.AddToClassList("backdrop");

            _messageLabel = new Label();
            _messageLabel.AddToClassList("message-label");
            _button = new Button(OnButtonClick);
            _button.AddToClassList("restart-button");
            
            _messagebox = new VisualElement();
            _messagebox.AddToClassList("message-box");
            _messagebox.AddToClassList("message-box-hidden");
            _messagebox.Add(_messageLabel);
            _messagebox.Add(_button);

            this.Add(backdrop);
            this.Add(_messagebox);
        }
        
        private void OnButtonClick() {
            var clickedEvent = new MessageboxButtonClicked(this);
            this.SendEvent(clickedEvent);
        }

        public void SetMessage(string message) {
            _messageLabel.text = message;
        }

        public void SetButtonText(string buttonText) {
            _button.text = buttonText;
        }
        
        public async Task ShowAsync() {
            await Task.Delay(TimeSettings.DELTA_TIME_MS); // delay one frame
            _messagebox.RemoveFromClassList("message-box-hidden");
            _messagebox.AddToClassList("message-box-shown");
            await Task.Delay(200);//duration of animation
        }

        public async Task HideAsync() {
            _messagebox.RemoveFromClassList("message-box-shown");
            _messagebox.AddToClassList("message-box-hidden");
            await Task.Delay(200);//duration of animation
        }
    }
}