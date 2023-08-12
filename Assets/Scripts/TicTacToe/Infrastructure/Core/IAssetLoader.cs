namespace TicTacToe.Infrastructure.Core {
    /// <summary>
    /// Provides an interface for loading assets of a specified type.
    /// This abstraction allows for different implementations that may use various techniques or platforms for asset loading.
    /// </summary>
    public interface IAssetLoader {
        T LoadAsset<T>(string assetName = "");
    }
}