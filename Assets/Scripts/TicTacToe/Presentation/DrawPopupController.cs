namespace TicTacToe.Presentation {
    public class DrawPopupController {
        private const string MESSAGE = "X O\nDRAW!";

        private readonly MessageboxPopup _popup;
        private readonly IPopupManager _popupManager;

        public DrawPopupController(MessageboxPopup popup, IPopupManager popupManager) {
            _popup = popup;
            _popupManager = popupManager;

            _popup.Opened += OnViewOpened;
        }

        private void OnViewOpened() {
            _popup.SetMessage(MESSAGE);

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