using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class WinPopupController {
        private const string WIN_TEXT_FORMAT = "{0} Won!";

        private readonly MessageboxPopup _popup;
        private readonly PlayerSymbol _winSymbol;
        private readonly IPopupManager _popupManager;

        public WinPopupController(MessageboxPopup winPopup, PlayerSymbol winSymbol, IPopupManager popupManager) {
            _popup = winPopup;
            _winSymbol = winSymbol;
            _popupManager = popupManager;

            _popup.RegisterCallback<AttachToPanelEvent>(OnViewOpened);
        }

        private void OnViewOpened(AttachToPanelEvent evt) {
            _popup.SetMessage(string.Format(WIN_TEXT_FORMAT, _winSymbol));

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