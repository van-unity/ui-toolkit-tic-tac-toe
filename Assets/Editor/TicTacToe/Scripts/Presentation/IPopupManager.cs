
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IPopupManager {
        VisualElement Container { get; }
        void HidePopupAsync<T>(T popup) where T: VisualElement, IPopup;
    }
}