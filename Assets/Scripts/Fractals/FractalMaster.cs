using Raymarching;
using UnityEngine;

namespace Fractals
{
    public class FractalMaster : MonoBehaviour
    {
        [SerializeField]
        private OnRenderImageDispatcher renderDispatcher;

        [SerializeField]
        private ComputeShader fractalShader;

        private RenderTexture target;
        private new Camera camera;

        private int threadGroupsX, threadGroupsY;

        private void Awake()
        {
            camera = Camera.main;

            threadGroupsX = Mathf.CeilToInt(Screen.width / 32);
            threadGroupsY = Mathf.CeilToInt(Screen.height / 32);

            target = RenderTextureCreator.Create();
            fractalShader.SetTexture(0, "Result", target);
            renderDispatcher.OnImageRendered += OnImageRendered;
        }

        private void OnImageRendered(RenderTexture source, RenderTexture destination)
        {
            SetShaderParameters();
            fractalShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
            Graphics.Blit(target, destination);
        }

        private void SetShaderParameters()
        {
            fractalShader.SetMatrix("CameraToWorld", camera.cameraToWorldMatrix);
            fractalShader.SetMatrix("CameraInverseProjection", camera.projectionMatrix.inverse);
        }
    }
}