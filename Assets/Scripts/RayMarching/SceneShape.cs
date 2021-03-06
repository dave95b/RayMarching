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

        [SerializeField, Range(0.5f, 3f)]
        private float radius = 1f;

        [SerializeField]
        private Operation operation;

        [SerializeField, Range(0f, 2f)]
        private float blendStrength = 0.5f;

        public Shape Shape
        {
            get
            {
                Vector3 vecColor = new Vector3(color.r, color.g, color.b);
                return new Shape(transform.position,
                    vecColor,
                    radius,
                    operation,
                    blendStrength,
                    transform.childCount);
            }
        }

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