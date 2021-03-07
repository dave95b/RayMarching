using System;
using UnityEngine;

namespace Raymarching
{
    public class SceneShape : MonoBehaviour
    {
        public event Action OnUpdated;

        public int Id;

        [SerializeField]
        private Color color;

        [SerializeField, Range(0f, 3f)]
        private float radius = 1f;

        [SerializeField]
        private Vector3 size;

        [SerializeField]
        private Operation operation;

        [SerializeField]
        private ShapeType type;

        [SerializeField, Range(0f, 2f)]
        private float blendStrength = 0.5f;

        public Shape Shape => new Shape(transform.position,
                    new Vector3(color.r, color.g, color.b),
                    size,
                    radius,
                    operation,
                    type,
                    blendStrength,
                    transform.childCount);

        private void Update()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                DispatchUpdate();
            }
        }

        private void OnValidate() => DispatchUpdate();

        private void DispatchUpdate() => OnUpdated?.Invoke();

        private void OnDestroy() => OnUpdated = null;
    }
}