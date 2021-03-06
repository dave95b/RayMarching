using System;
using System.Collections;
using UnityEngine;

namespace Raymarching
{
    public class ShapeController : MonoBehaviour
    {
        public event Action<Shape[]> OnShapesUpdated;

        private SceneShape[] sceneShapes;
        private Shape[] shapes;

        private void Start()
        {
            FindShapes();
        }

        private void FindShapes()
        {
            sceneShapes = GetComponentsInChildren<SceneShape>();
            shapes = new Shape[sceneShapes.Length];
            UpdateShapes();

            foreach (var sceneShape in sceneShapes)
                sceneShape.OnUpdated += UpdateShapes;
        }

        private void UpdateShapes()
        {
            for (int i = 0; i < sceneShapes.Length; i++)
            {
                sceneShapes[i].Id = i;
                shapes[i] = sceneShapes[i].Shape;
            }

            OnShapesUpdated?.Invoke(shapes);
        }

        private void OnDestroy() => OnShapesUpdated = null;
    }
}