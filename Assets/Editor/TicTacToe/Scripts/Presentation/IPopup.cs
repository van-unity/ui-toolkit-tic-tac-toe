using System.Threading.Tasks;

namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IPopup {
        Task ShowAsync();
        Task HideAsync();
    }
}