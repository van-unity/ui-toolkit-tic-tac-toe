using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class AnimatedLine : VisualElement {
        public float Width { get; }

        public AnimatedLine(Vector2 from, Vector2 to) {
            if (to.x < from.x) {
                (to, from) = (from, to);
            }

            // var width = Vector2.Distance(from, to);

            var direction = to - from;
            var angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Width = direction.magnitude;
            var left = from.x;
            var top = from.y;

            this.style.position = Position.Absolute;
            this.style.left = left;
            this.style.top = top;
            this.style.width = Width;
            this.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(0, LengthUnit.Percent),
                new Length(50, LengthUnit.Percent)));
           
            
            this.transform.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
            // this.schedule
            //     .Execute(() => {
            //         this.style.width = width;
            //     })
            //     .ExecuteLater(16);
        }
    }
}