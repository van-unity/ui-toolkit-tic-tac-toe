using TicTacToe.Presentation.UiToolkit.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit {
    public class PopupsContainer : VisualElement{
        public PopupsContainer(IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.PopupsContainerStyle);
            this.AddToClassList("popups-container");
        }
    }
}