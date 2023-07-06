using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class AnimatedLine : VisualElement {
        private const float HUNDRED_PIXEL_DURATION = 0.075f;
        private const EasingMode EASING_MODE = EasingMode.EaseOutSine;

        public AnimatedLine(Vector2 from, Vector2 to) {
            if (to.x < from.x) {
                (to, from) = (from, to);
            }

            // var width = Vector2.Distance(from, to);

            var direction = to - from;
            var angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var width = direction.magnitude;
            var left = from.x;
            var top = from.y;
            var durationInSeconds = width / 100 * HUNDRED_PIXEL_DURATION;

            this.style.width = 1;
            this.style.position = Position.Absolute;
            this.style.left = left;
            this.style.top = top;
            this.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(0, LengthUnit.Percent),
                new Length(50, LengthUnit.Percent)));
            this.style.transitionProperty = new StyleList<StylePropertyName>(new List<StylePropertyName>() { "width" });
            this.style.transitionTimingFunction = new StyleList<EasingFunction>(new List<EasingFunction>()
                { new(EASING_MODE) });
            this.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue>()
                { new(durationInSeconds, TimeUnit.Second) });
            this.transform.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
            this.schedule
                .Execute(() => {
                    this.style.width = width;
                })
                .ExecuteLater(16);
        }
    }
}