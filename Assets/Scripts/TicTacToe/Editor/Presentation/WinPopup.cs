using System;
using System.Threading.Tasks;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public interface IPopup {
        void Show(Action callback = null);
        void Hide(Action callback = null);
    }


    public class WinPopup : VisualElement, IPopup {
        private readonly Label _winnerLabel;
        private readonly VisualElement _box;

        public WinPopup() {
            this.SetStyleFromPath("WinPopup");
            this.AddToClassList("win-popup");

            var backdrop = new VisualElement();
            backdrop.AddToClassList("backdrop");

            _winnerLabel = new Label();
            _winnerLabel.AddToClassList("winner-label");
            var winnerLabelStroke = new Label();
            winnerLabelStroke.AddToClassList("winner-label-stroke");
            _winnerLabel.Add(winnerLabelStroke);
            var restartButton = new Button(OnRestartButtonClick) {
                text = "Restart"
            };
            restartButton.AddToClassList("restart-button");
            _box = new VisualElement();
            _box.AddToClassList("box");
            _box.AddToClassList("box-hidden");
            _box.Add(_winnerLabel);
            _box.Add(restartButton);

            this.Add(backdrop);
            this.Add(_box);
        }

        public void SetWinnerText(string winnerText) {
            _winnerLabel.text = winnerText;
            _winnerLabel.Q<Label>().text = winnerText;
        }

        private void OnRestartButtonClick() {
            var clickedEvent = new RestartButtonClicked(this);
            this.SendEvent(clickedEvent);
        }

        public async void Show(Action callback = null) {
            await Task.Delay(100);
            _box.AddToClassList("box-shown");
        }

        public void Hide(Action callback = null) {
            _box.AddToClassList("box-hidden");
        }
    }
}