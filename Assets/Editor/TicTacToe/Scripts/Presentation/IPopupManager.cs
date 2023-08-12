namespace Editor.TicTacToe.Scripts.Presentation {
    public interface IPopupManager {
        void HidePopupAsync<T>(T popup) where T : IPopup;
    }
}