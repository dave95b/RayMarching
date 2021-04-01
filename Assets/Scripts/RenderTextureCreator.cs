using System.Collections.Generic;
using UnityEngine;
public static class RenderTextureCreator
{
    public static RenderTexture Create()
    {
        var texture = new RenderTexture(Screen.width, Screen.height, 0);
        texture.enableRandomWrite = true;
        texture.Create();

        return texture;
    }
}
