using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public interface IPopupManager {
        VisualElement Container { get; }
        Task ShowWinPopupAsync(Symbol winSymbol);
        Task ShowDrawPopupAsync();
        void HidePopupAsync(IPopup popup);
    }
}