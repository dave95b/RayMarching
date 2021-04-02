using Raymarching;
using System.Diagnostics;
using UnityEngine;

namespace Fractals
{
    public class FractalMaster : MonoBehaviour
    {
        [SerializeField]
        private OnRenderImageDispatcher renderDispatcher;

        [SerializeField]
        private LightSource lightSource;

        [SerializeField]
        private ComputeShader fractalShader;

        [Header("Fractal settings")]
        [SerializeField, Range(2, 32)]
        private float power = 8;

        [SerializeField, Range(2, 100)]
        private float darkness = 70;

        [SerializeField, Range(0, 1)]
        private float blackAndWhite = 0.5f;

        [SerializeField]
        private Color colorMix;

        private RenderTexture target;
        private new Camera camera;

        private int threadGroupsX, threadGroupsY;

        private Stopwatch s;

        private void Awake()
        {
            camera = Camera.main;

            threadGroupsX = Mathf.CeilToInt(Screen.width / 32);
            threadGroupsY = Mathf.CeilToInt(Screen.height / 32);

            target = RenderTextureCreator.Create();
            fractalShader.SetTexture(0, "Result", target);
            renderDispatcher.OnImageRendered += OnImageRendered;

            lightSource.OnLightChanged += OnLightChanged;
        }

        //private void Update()
        //{
        //    s.Stop();

        //    UnityEngine.Debug.Log($"Render time: {s.Elapsed.TotalMilliseconds} ms");
        //}

        private void OnImageRendered(RenderTexture source, RenderTexture destination)
        {
            SetShaderParameters();
            s = Stopwatch.StartNew();
            fractalShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
            Graphics.Blit(target, destination);
        }

        private void SetShaderParameters()
        {
            fractalShader.SetMatrix("CameraToWorld", camera.cameraToWorldMatrix);
            fractalShader.SetMatrix("CameraInverseProjection", camera.projectionMatrix.inverse);
            fractalShader.SetFloat("Power", power);
            fractalShader.SetFloat("Darkness", darkness);
            fractalShader.SetFloat("BlackAndWhite", blackAndWhite);
            fractalShader.SetVector("ColorMix", FromColor(colorMix));
        }

        private void OnLightChanged(LightData data)
        {
            fractalShader.SetFloat("LightIntensity", data.Intensity);
            fractalShader.SetVector("LightColor", FromColor(data.Color));
            fractalShader.SetVector("LightDirection", data.Direction);
        }

        private Vector3 FromColor(Color color) => new Vector3(color.r, color.g, color.b);
    }
}