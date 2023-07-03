using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Utils {
    public static class LineDrawer {
        public static VisualElement DrawLine(Vector2 from, Vector2 to, float durationInSeconds,
            EasingMode easingMode = EasingMode.Linear, float delay = 0) {
            if (to.y < from.y) {
                (to, from) = (from, to);
            }

            // var width = Vector2.Distance(from, to);

            var direction = to - from;
            var angleDeg = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            var width = direction.magnitude;
            var left = from.y;
            var top = from.x;
            durationInSeconds = width * .00075f;
            var line = new VisualElement {
                style = {
                    width = 1,
                    position = Position.Absolute,
                    left = left,
                    top = top,
                    transformOrigin = new StyleTransformOrigin(new TransformOrigin(0, 1)),
                    transitionProperty = new StyleList<StylePropertyName>(new List<StylePropertyName>() { "width" }),
                    transitionTimingFunction = new StyleList<EasingFunction>(new List<EasingFunction>()
                        { new(easingMode) }),
                    transitionDuration = new StyleList<TimeValue>(new List<TimeValue>()
                        { new(durationInSeconds, TimeUnit.Second) }),
                    transitionDelay = new StyleList<TimeValue>(new List<TimeValue>() { new(delay, TimeUnit.Second) })
                }
            };

            // line.AddToClassList("winning-line");
            line.schedule.Execute(() => { line.style.width = width; }).ExecuteLater(100);
            line.transform.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);

            return line;
        }
    }
}