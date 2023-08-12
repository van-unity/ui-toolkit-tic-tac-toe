using TicTacToe.Presentation.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation {
    public class PopupsContainer : VisualElement{
        public PopupsContainer(IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.PopupsContainerStyle);
            this.AddToClassList("popups-container");
        }
    }
}