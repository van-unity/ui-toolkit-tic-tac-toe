using TicTacToe.Domain;

namespace TicTacToe.Presentation {
    public class WinPopupController {
        private const string WIN_TEXT_FORMAT = "{0} Won!";

        private readonly MessageboxPopup _popup;
        private readonly PlayerSymbol _winSymbol;
        private readonly IPopupManager _popupManager;

        public WinPopupController(MessageboxPopup winPopup, PlayerSymbol winSymbol, IPopupManager popupManager) {
            _popup = winPopup;
            _winSymbol = winSymbol;
            _popupManager = popupManager;

            _popup.Opened += OnViewOpened;
        }

        private void OnViewOpened() {
            _popup.SetMessage(string.Format(WIN_TEXT_FORMAT, _winSymbol));

            _popup.OkButtonClicked += OnOkButtonClicked;
            _popup.Closed += OnViewClosed;
        }

        private void OnOkButtonClicked() {
            _popupManager.HidePopupAsync(_popup);
        }

        private void OnViewClosed() {
            _popup.Opened -= OnViewClosed;
            _popup.OkButtonClicked -= OnOkButtonClicked;
            _popup.Closed -= OnViewClosed;
        }
    }
}