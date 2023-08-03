using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PopupManager : IPopupManager {
        private readonly IStyleSettings _styleSettings;
        public VisualElement Container { get; }

        public PopupManager(VisualElement container, IStyleSettings styleSettings) {
            _styleSettings = styleSettings;
            Container = container;
            container.visible = false;
        }

        public async Task ShowWinPopupAsync(Symbol winSymbol) {
            Container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new WinPopupController(popup, winSymbol, this);
            Container.Add(popup);
            await popup.ShowAsync();
        }

        public async Task ShowDrawPopupAsync() {
            Container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new DrawPopupController(popup, this);
            Container.Add(popup);
            await popup.ShowAsync();
        }

        public async void HidePopupAsync(IPopup popup) {
            await popup.HideAsync();
            var visualElement = popup as VisualElement;
            visualElement.Clear();
            Container.Remove(visualElement);
            Container.visible = false;
        }
    }
}