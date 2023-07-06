using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Utils {
    public static class StyleHelperMethods {
        internal static void SetStyleFromPath(VisualElement element, string stylePath) {
            // If the attribute exists and has a valid file name, search for the stylesheet.
            if (!string.IsNullOrEmpty(stylePath)) {
                string[] guids = AssetDatabase.FindAssets($"{stylePath} t:StyleSheet");

                if (guids.Length > 0) {
                    // Load the first found stylesheet.
                    var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
                    element.styleSheets.Clear();
                    element.styleSheets.Add(styleSheet);
                }
                else {
                    Debug.LogWarning($"Failed to find stylesheet with the name: {stylePath}");
                }
            }
            else {
                throw new WarningException("Empty style path!");
            }
        }
    }
}