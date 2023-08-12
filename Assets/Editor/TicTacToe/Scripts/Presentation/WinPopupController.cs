using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class WinPopupController {
        private const string WIN_TEXT_FORMAT = "{0} Won!";

        private readonly MessageboxPopup _popup;
        private readonly PlayerSymbol _winSymbol;
        private readonly PopupManager _popupManager;

        public WinPopupController(MessageboxPopup winPopup, PlayerSymbol winSymbol, PopupManager popupManager) {
            _popup = winPopup;
            _winSymbol = winSymbol;
            _popupManager = popupManager;
            _popup.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _popup.RegisterCallback<MessageboxCloseButtonClicked>(OnRestartButtonClicked);
            _popup.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
        }

        private void ViewOpened(AttachToPanelEvent evt) {
            _popup.SetMessage(string.Format(WIN_TEXT_FORMAT, _winSymbol));
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