using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PopupManager {
        public VisualElement Container { get; }

        public PopupManager(VisualElement container) {
            Container = container;
            container.visible = false;
        }

        public void ShowWinPopup(IGameController gameController, Win win) {
            Container.visible = true;
            var winPopup = new WinPopup();
            var popupController = new WinPopupController(winPopup, gameController, win, this);
            Container.Add(winPopup);
            winPopup.Show();
        }

        public void HidePopup(VisualElement popup) {
            popup.Clear();
            Container.Remove(popup);
            Container.visible = false;
        }
    }
}