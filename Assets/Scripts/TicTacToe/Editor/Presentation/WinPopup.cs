using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class WinPopup : VisualElement {
        private readonly Label _winnerLabel;
        private readonly Button _restartButton;
        
        public WinPopup() {
            this.SetStyleFromPath("WinPopup");
            
        }
    }
}