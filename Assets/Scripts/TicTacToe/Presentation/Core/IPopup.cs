using System.Threading.Tasks;

namespace TicTacToe.Presentation.Core {
    public interface IPopup {
        Task ShowAsync();
        Task HideAsync();
    }
}