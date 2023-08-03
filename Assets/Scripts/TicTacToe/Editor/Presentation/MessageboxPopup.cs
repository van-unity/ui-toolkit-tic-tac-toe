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
        
        public MessageboxPopup(IStyleSettings styleSettings) {
            this.SetStyleFromPath(styleSettings.MessageboxPopupStyle);
            this.AddToClassList("popup");

            var backdrop = new VisualElement();
            backdrop.AddToClassList("backdrop");
            backdrop.RegisterCallback<ClickEvent>(_ => OnCloseButtonClick());
            _messageLabel = new Label();
            _messageLabel.AddToClassList("message-label");
            
            _messagebox = new VisualElement();
            _messagebox.AddToClassList("message-box");
            _messagebox.AddToClassList("message-box-hidden");
            _messagebox.Add(_messageLabel);

            this.Add(backdrop);
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
            await Task.Delay(200);//duration of animation
        }

        public async Task HideAsync() {
            _messagebox.RemoveFromClassList("message-box-shown");
            _messagebox.AddToClassList("message-box-hidden");
            await Task.Delay(200);//duration of animation
        }
    }
}