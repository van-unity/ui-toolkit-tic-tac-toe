using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class Popup : VisualElement {
        public Popup() {
            this.SetStyleFromPath("Popup");
            this.AddToClassList("popup");
        }
    }
}