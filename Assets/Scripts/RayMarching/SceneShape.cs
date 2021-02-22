using System;
using System.Collections;
using UnityEngine;

namespace Raymarching
{
    public class SceneShape : MonoBehaviour
    {
        public event Action OnUpdated;
        
        [SerializeField]
        private Color color;

        [SerializeField, Range(0.5f, 3f)]
        private float radius = 1f;

        public Shape Shape
        {
            get
            {
                Vector3 vecColor = new Vector3(color.r, color.g, color.b);
                return new Shape(transform.position, vecColor, radius);
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