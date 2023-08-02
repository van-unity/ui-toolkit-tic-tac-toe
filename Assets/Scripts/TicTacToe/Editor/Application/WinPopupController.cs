using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Application {
    public class WinPopupController {
        private const string WIN_TEXT_FORMAT = "{0} Won!";

        private readonly WinPopup _popup;
        private readonly IGameController _gameController;
        private readonly Win _win;
        private readonly PopupManager _popupManager;

        public WinPopupController(WinPopup winPopup, IGameController gameController, Win win,
            PopupManager popupManager) {
            _popup = winPopup;
            _gameController = gameController;
            _win = win;
            _popupManager = popupManager;
            _popup.RegisterCallback<AttachToPanelEvent>(ViewOpened);
        }

        private void ViewOpened(AttachToPanelEvent evt) {
            _popup.SetWinnerText(string.Format(WIN_TEXT_FORMAT, _win.Symbol));
            _popup.RegisterCallback<RestartButtonClicked>(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked(RestartButtonClicked evt) {
            _gameController.Restart();
            _popupManager.HidePopup(_popup);
        }
    }
}