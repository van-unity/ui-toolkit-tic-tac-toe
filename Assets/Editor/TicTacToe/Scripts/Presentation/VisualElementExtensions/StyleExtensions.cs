using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions {
    public static class StyleExtensions {
        internal static void SetStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Clear();
            element.styleSheets.Add(styleSheet);
        }

        internal static void AddToStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Add(styleSheet);
        }
    }
}