using System;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor.Application {
    public static class AssetLoader {
        public static T LoadAsset<T>() where T : UnityEngine.Object {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            switch (guids.Length) {
                case > 1:
                    Debug.LogWarning($"Multiple {typeof(T).Name} found!");
                    break;
                case 0:
                    throw new NullReferenceException($"Unable to find {typeof(T).Name}!");
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
    }
}