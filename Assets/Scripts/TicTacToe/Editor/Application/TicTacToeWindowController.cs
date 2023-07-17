using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;

namespace TicTacToe.Editor.Application {
    public static class TicTacToeWindowController {
        private const int DEFAULT_SIZE = 3;
        private const int MIN_SIZE = 3;
        
        private static BoardModel _boardModel;
        private static TicTacToeWindow _view;
        
        public static event Action<BoardModel> BoardModelUpdated;

        public static void OnViewWasEnabled(TicTacToeWindow view) {
            
        }
        
        
        public static void OnSizeChanged(int newSize) {
            if (newSize < MIN_SIZE) {
                return;
            }

            _boardModel = new BoardModel(newSize);
            
            BoardModelUpdated?.Invoke(_boardModel);
        }
    }
}