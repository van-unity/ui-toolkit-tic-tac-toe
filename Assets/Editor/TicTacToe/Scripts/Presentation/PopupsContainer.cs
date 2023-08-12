using Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class PopupsContainer : VisualElement{
        public PopupsContainer(IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.PopupsContainerStyle);
            this.AddToClassList("popups-container");
        }
    }
}