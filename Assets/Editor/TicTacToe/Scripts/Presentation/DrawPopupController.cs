using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class DrawPopupController {
        private const string MESSAGE = "X O\nDRAW!";
        
        private readonly MessageboxPopup _popup;
        private readonly IPopupManager _popupManager;
        
        public DrawPopupController(MessageboxPopup popup, IPopupManager popupManager) {
            _popup = popup;
            _popupManager = popupManager;
            _popup.RegisterCallback<AttachToPanelEvent>(OnViewOpened);
        }
        
        private void OnViewOpened(AttachToPanelEvent evt) {
            _popup.SetMessage(MESSAGE);
            
            _popup.RegisterCallback<MessageboxCloseButtonClicked>(OnRestartButtonClicked);
            _popup.RegisterCallback<DetachFromPanelEvent>(OnViewClosed);
        }

        private void OnRestartButtonClicked(MessageboxCloseButtonClicked evt) {
            _popupManager.HidePopupAsync(_popup);
        }

        private void OnViewClosed(DetachFromPanelEvent evt) {
            _popup.UnregisterCallback<AttachToPanelEvent>(OnViewOpened);
            _popup.UnregisterCallback<MessageboxCloseButtonClicked>(OnRestartButtonClicked);
            _popup.UnregisterCallback<DetachFromPanelEvent>(OnViewClosed);
        }
    }
}