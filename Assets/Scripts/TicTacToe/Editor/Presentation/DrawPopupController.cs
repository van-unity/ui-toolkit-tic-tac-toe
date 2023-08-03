using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class DrawPopupController {
        private const string MESSAGE = "X O\nDRAW!";
        
        private readonly MessageboxPopup _popup;
        private readonly IPopupManager _popupManager;
        
        public DrawPopupController(MessageboxPopup popup, IPopupManager popupManager) {
            _popup = popup;
            _popupManager = popupManager;
            _popup.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _popup.RegisterCallback<MessageboxCloseButtonClicked>(OnRestartButtonClicked);
            _popup.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
        }
        
        private void ViewOpened(AttachToPanelEvent evt) {
            _popup.SetMessage(MESSAGE);
        }

        private void OnRestartButtonClicked(MessageboxCloseButtonClicked evt) {
            _popupManager.HidePopupAsync(_popup);
        }

        private void ViewClosed(DetachFromPanelEvent evt) {
            _popup.UnregisterCallback<AttachToPanelEvent>(ViewOpened);
            _popup.UnregisterCallback<MessageboxCloseButtonClicked>(OnRestartButtonClicked);
            _popup.UnregisterCallback<DetachFromPanelEvent>(ViewClosed);
        }
    }
}