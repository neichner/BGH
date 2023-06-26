using System.Linq;
using UnityEngine;

namespace NE {
    public class TouchListener {
        private Touch[] EmulateEditorTouch() {
            Touch mainTouch = new Touch();
            mainTouch.position = Input.mousePosition;
            if(Input.GetMouseButtonDown(0)) {
                mainTouch.phase = TouchPhase.Began;
            }
            else if(Input.GetMouseButton(0)) {
                mainTouch.phase = TouchPhase.Moved;
            }
            else if(Input.GetMouseButtonUp(0)) {
                mainTouch.phase = TouchPhase.Ended;
            }
            else {
                return new Touch[] {};
            }
            return new Touch[] { mainTouch };
        }

        public bool IsDragging() {
            return IsDragging(out _);
        }

        public bool IsDragging(out Touch outTouch) {
            var touches = Input.touches;
            #if UNITY_EDITOR
            touches = EmulateEditorTouch();
            #endif
            outTouch = touches.FirstOrDefault();
            if(touches.Length == 0) 
                return false;
            return touches.First().phase == TouchPhase.Moved;
        }

        public bool IsZooming(out float zoomDelta)
        {
            if(Input.touchCount == 2 && (
                Input.GetTouch(0).phase == TouchPhase.Moved || 
                Input.GetTouch(1).phase == TouchPhase.Moved))
            {
                var deltaMovement = (Input.GetTouch(0).deltaPosition + Input.GetTouch(0).deltaPosition);
                zoomDelta = deltaMovement.x + deltaMovement.y;
                return true;
            }
#if UNITY_EDITOR
            zoomDelta = Input.mouseScrollDelta.y;
            return Input.mouseScrollDelta.y != 0f;
#else
            return false;
#endif
        }
    }
}