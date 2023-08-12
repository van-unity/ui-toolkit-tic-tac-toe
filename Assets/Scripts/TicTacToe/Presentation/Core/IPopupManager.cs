namespace TicTacToe.Presentation.Core {
    public interface IPopupManager {
        void HidePopupAsync<T>(T popup) where T : IPopup;
    }
}