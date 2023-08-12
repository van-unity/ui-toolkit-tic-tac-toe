using System;
using UnityEditor;
using UnityEngine;

namespace Editor.TicTacToe.Scripts.Infrastructure {
    /// <summary>
    /// Implementation of the <see cref="IAssetLoader"/> interface using Unity's AssetDatabase.
    /// Provides functionality to load assets of a specific type that is a UnityEngine.Object or derives from it.
    /// </summary>
    public class AssetDatabaseAssetLoader : IAssetLoader {
        public T LoadAsset<T>() {
            if (!(typeof(T).IsSubclassOf(typeof(UnityEngine.Object)) || typeof(T) == typeof(UnityEngine.Object))) {
                throw new InvalidOperationException(
                    $"Type {typeof(T).Name} must be a UnityEngine.Object or a type that derives from it.");
            }

            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            switch (guids.Length) {
                case > 1:
                    Debug.LogWarning($"Multiple {typeof(T).Name} found!");
                    break;
                case 0:
                    throw new NullReferenceException($"Unable to find {typeof(T).Name}!");
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return (T)(object)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
    }
}