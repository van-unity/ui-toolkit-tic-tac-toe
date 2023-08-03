using System.Threading.Tasks;

namespace TicTacToe.Editor.Presentation {
    public interface IPopup {
        Task ShowAsync();
        Task HideAsync();
    }
}