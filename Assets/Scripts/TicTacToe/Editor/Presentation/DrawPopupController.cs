using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class DrawPopupController {
        private const string BUTTON_TEXT = "Restart";
        private const string MESSAGE = "X O\nDRAW!";
        
        private readonly MessageboxPopup _popup;
        private readonly IPopupManager _popupManager;
        private readonly IGameController _gameController;
        
        public DrawPopupController(MessageboxPopup popup, IPopupManager popupManager, IGameController gameController) {
            _popup = popup;
            _popupManager = popupManager;
            _gameController = gameController;
            _popup.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _popup.RegisterCallback<MessageboxButtonClicked>(OnRestartButtonClicked);
            _popup.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
        }
        
        private void ViewOpened(AttachToPanelEvent evt) {
            _popup.SetButtonText(BUTTON_TEXT);
            _popup.SetMessage(MESSAGE);
        }

        private void OnRestartButtonClicked(MessageboxButtonClicked evt) {
            _gameController.Restart();
            _popupManager.HidePopupAsync(_popup);
        }

        private void ViewClosed(DetachFromPanelEvent evt) {
            _popup.UnregisterCallback<AttachToPanelEvent>(ViewOpened);
            _popup.UnregisterCallback<MessageboxButtonClicked>(OnRestartButtonClicked);
            _popup.UnregisterCallback<DetachFromPanelEvent>(ViewClosed);
        }
    }
}