using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions {
    public static class StyleExtensions {
        public static void SetStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Clear();
            element.styleSheets.Add(styleSheet);
        }

        public static void AddToStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Add(styleSheet);
        }
    }
}