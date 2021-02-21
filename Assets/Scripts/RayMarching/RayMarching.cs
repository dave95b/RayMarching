using Raymarching;
using UnityEngine;

public class RayMarching : MonoBehaviour
{
    [SerializeField]
    private OnRenderImageDispatcher renderDispatcher;

    [SerializeField]
    private ComputeShader rayMarchingShader;

    private RenderTexture target;
    private new Camera camera;

    private int threadGroupsX, threadGroupsY;

    private void Start()
    {
        camera = Camera.main;
        Debug.LogError(camera.transform.forward);
        Debug.LogError(camera.transform.position);

        threadGroupsX = Mathf.CeilToInt(Screen.width / 32);
        threadGroupsY = Mathf.CeilToInt(Screen.height / 32);

        target = CreateRenderTexture();
        rayMarchingShader.SetTexture(0, "Result", target);
        renderDispatcher.OnImageRendered += OnImageRendered;
    }

    private void OnImageRendered(RenderTexture source, RenderTexture destination)
    {
        SetShaderParameters();
        rayMarchingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
        Graphics.Blit(target, destination);
    }

    private void SetShaderParameters()
    {
        rayMarchingShader.SetMatrix("CameraToWorld", camera.cameraToWorldMatrix);
        rayMarchingShader.SetMatrix("CameraInverseProjection", camera.projectionMatrix.inverse);
    }

    private RenderTexture CreateRenderTexture()
    {
        var result = new RenderTexture(Screen.width, Screen.height, 0);
        result.enableRandomWrite = true;
        result.Create();

        return result;
    }
}
