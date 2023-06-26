using UnityEngine;
using System.Linq;
using NE.Config;

namespace NE.Behaviour.CameraBehaviour
{
    [RequireComponent(typeof(Camera))]
    public class CanvasDrag : MonoBehaviour
    {
        [SerializeField] private StateMachine uiStateMachine;
        [SerializeField] private State worksOnlyIfUIStateIs;
        [SerializeField] private float zoomSpeed = 1f;
        [SerializeField] private float minZoom = 10f;

        private Camera mainCamera = null;
        private TouchListener touchListener = new TouchListener();

        private Vector2? previousPosition = null;
        private Vector2 min;
        private Vector2 max;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
            if (mainCamera == null)
            {
                throw new System.Exception("Expected camera");
            }
            CalculateMaxCameraExtents();
        }

        private void Update()
        {
            if (uiStateMachine.CurrentState != worksOnlyIfUIStateIs)
                return;
            DragUpdate();
            ScrollUpdate();
        }

        private void CalculateMaxCameraExtents()
        {
            var verticalExtent = mainCamera.orthographicSize;
            var horizontalExtent = verticalExtent * Screen.width / Screen.height;
            min = new Vector2(horizontalExtent - Screen.width / 2.0f, verticalExtent - Screen.height / 2.0f);
            max = new Vector2(Screen.width / 2.0f - horizontalExtent, Screen.height / 2.0f - verticalExtent);
        }

        private void ScrollUpdate()
        {
            if (!touchListener.IsZooming(out float deltaZoom)) return;
            mainCamera.orthographicSize += deltaZoom * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, Screen.height / 2f);
            CalculateMaxCameraExtents();
            ClampCamera();
        }

        private void ClampCamera()
        {
            var clampedPosition = new Vector3(
                        Mathf.Clamp(transform.position.x, min.x, max.x),
                        Mathf.Clamp(transform.position.y, min.y, max.y),
                        transform.position.z
                        );
            transform.position = clampedPosition;
        }

        private void DragUpdate()
        {
            if (touchListener.IsDragging(out Touch touch))
            {
                if (previousPosition != null)
                {
                    transform.position -= (Vector3)(touch.position - previousPosition.Value);
                    ClampCamera();
                }
                previousPosition = touch.position;
            }
            else
            {
                previousPosition = null;
            }
        }
    }
}