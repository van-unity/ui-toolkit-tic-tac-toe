using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public interface IPopupManager {
        VisualElement Container { get; }
        Task ShowWinPopupAsync(IGameController gameController, Symbol winSymbol);
        Task ShowDrawPopupAsync(IGameController gameController);
        void HidePopupAsync(IPopup popup);
    }
}