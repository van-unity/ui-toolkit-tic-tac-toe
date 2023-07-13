using TicTacToe.Editor.Utils;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class Popup : VisualElement {
        public Popup() {
            StyleHelperMethods.SetStyleFromPath(this, "Popup");
            this.AddToClassList("popup");
        }
    }
}