using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class PopupsContainer : VisualElement{
        public PopupsContainer(IStyleSettings styleSettings) {
            this.SetStyleFromPath(styleSettings.PopupsContainerStyle);
            this.AddToClassList("popups-container");
        }
    }
}