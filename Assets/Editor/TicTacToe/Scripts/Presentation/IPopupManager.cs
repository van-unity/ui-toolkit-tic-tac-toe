using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IPopupManager {
        VisualElement Container { get; }
        void HidePopupAsync(IPopup popup);
    }
}