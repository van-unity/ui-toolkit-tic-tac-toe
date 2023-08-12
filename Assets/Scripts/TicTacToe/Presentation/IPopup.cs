using System.Threading.Tasks;

namespace TicTacToe.Presentation {
    public interface IPopup {
        Task ShowAsync();
        Task HideAsync();
    }
}