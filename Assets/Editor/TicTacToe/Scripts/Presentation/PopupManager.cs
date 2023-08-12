using System;
using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class PopupManager : IPopupManager, IDisposable {
        private const int POPUPS_DELAY_MS = 750;

        private readonly IStyleSettings _styleSettings;
        private readonly IGameEvents _gameEvents;
        public VisualElement Container { get; }

        public PopupManager(VisualElement rootVisualElement, IStyleSettings styleSettings, IGameEvents gameEvents) {
            _styleSettings = styleSettings;
            _gameEvents = gameEvents;
            Container = new PopupsContainer(styleSettings);
            rootVisualElement.Add(Container);
            Container.visible = false;
            
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
            Container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new WinPopupController(popup, winSymbol, this);
            Container.Add(popup);
            await popup.ShowAsync();
        }

        private async Task ShowDrawPopupAsync() {
            Container.visible = true;
            var popup = new MessageboxPopup(_styleSettings);
            var popupController = new DrawPopupController(popup, this);
            Container.Add(popup);
            await popup.ShowAsync();
        }

        public async void HidePopupAsync<T>(T popup) where T: VisualElement, IPopup{
            await popup.HideAsync();
            var visualElement = popup as VisualElement;
            visualElement.Clear();
            Container.Remove(visualElement);
            Container.visible = false;
        }

        public void Dispose() {
            _gameEvents.GameWon -= OnGameWon;
            _gameEvents.GameDraw -= OnGameDraw;
        }
    }
}