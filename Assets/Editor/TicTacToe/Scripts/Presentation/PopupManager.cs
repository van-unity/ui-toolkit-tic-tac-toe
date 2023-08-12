using System;
using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class PopupManager : IPopupManager, IDisposable {
        private const int POPUPS_DELAY_MS = 750;

        private readonly IStyleSettings _styleSettings;
        private readonly IGameEvents _gameEvents;
        private readonly VisualElement _container;

        public PopupManager(VisualElement rootVisualElement, IStyleSettings styleSettings, IGameEvents gameEvents) {
            _styleSettings = styleSettings;
            _gameEvents = gameEvents;

            _container = new PopupsContainer(styleSettings);
            _container.visible = false;

            rootVisualElement.Add(_container);

            _gameEvents.GameWon += OnGameWon;
            _gameEvents.GameDraw += OnGameDraw;
        }

        private async void OnGameDraw() {
            await Task.Delay(POPUPS_DELAY_MS);

            await ShowDrawPopupAsync();
        }

        private async void OnGameWon(Win win) {
            await Task.Delay(POPUPS_DELAY_MS);

            await ShowWinPopupAsync(win.Symbol);
        }

        private async Task ShowWinPopupAsync(PlayerSymbol winSymbol) {
            _container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new WinPopupController(popup, winSymbol, this);
            _container.Add(popup);
            await popup.ShowAsync();
        }

        private async Task ShowDrawPopupAsync() {
            _container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new DrawPopupController(popup, this);
            _container.Add(popup);
            await popup.ShowAsync();
        }

        public async void HidePopupAsync<T>(T popup) where T : IPopup {
            await popup.HideAsync();
            var visualElement = popup as VisualElement;
            _container.Remove(visualElement);
            _container.visible = false;
        }

        public void Dispose() {
            _gameEvents.GameWon -= OnGameWon;
            _gameEvents.GameDraw -= OnGameDraw;
        }
    }
}