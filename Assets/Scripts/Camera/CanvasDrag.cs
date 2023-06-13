using UnityEngine;
using System.Linq;

namespace NE.CameraScripts {

    [RequireComponent(typeof(Camera))]
    public class CanvasDrag : MonoBehaviour
    {
        Camera mainCamera = null;
        TouchListener touchListener = new TouchListener();
        void Awake() {
            mainCamera = GetComponent<Camera>();
            if(mainCamera == null) {
                throw new System.Exception("Expected camera");
            }
        }

        void Update()
        {
            DragUpdate();
        }

        private void DragUpdate() {
            Touch touch;
            if(touchListener.IsDragging(out touch)) {
                Debug.Log(touch.position);
            }
        }
    }
}