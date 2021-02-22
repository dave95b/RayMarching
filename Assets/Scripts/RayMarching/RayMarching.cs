using Raymarching;
using UnityEngine;

public class RayMarching : MonoBehaviour
{
    [SerializeField]
    private OnRenderImageDispatcher renderDispatcher;

    [SerializeField]
    private ComputeShader rayMarchingShader;

    [SerializeField]
    private ShapeController shapeController;

    private RenderTexture target;
    private new Camera camera;

    private int threadGroupsX, threadGroupsY;

    private ComputeBuffer shapeBuffer;

    private bool updated = true;

    private void Awake()
    {
        camera = Camera.main;

        threadGroupsX = Mathf.CeilToInt(Screen.width / 32);
        threadGroupsY = Mathf.CeilToInt(Screen.height / 32);

        target = CreateRenderTexture();
        rayMarchingShader.SetTexture(0, "Result", target);
        renderDispatcher.OnImageRendered += OnImageRendered;

        shapeController.OnShapesUpdated += OnShapesUpdated;
    }

    private void OnImageRendered(RenderTexture source, RenderTexture destination)
    {
        if (!updated)
            return;

        SetShaderParameters();
        rayMarchingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
        Graphics.Blit(target, destination);

        updated = false;
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

    private void OnShapesUpdated(Shape[] shapes)
    {
        if (shapeBuffer == null)
        {
            shapeBuffer = new ComputeBuffer(shapes.Length, Shape.SizeOf());
            rayMarchingShader.SetBuffer(0, "Shapes", shapeBuffer);
        }

        shapeBuffer.SetData(shapes);

        updated = true;
    }
}
