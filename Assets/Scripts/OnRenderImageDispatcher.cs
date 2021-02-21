using System;
using UnityEngine;

namespace Raymarching
{
    public class OnRenderImageDispatcher : MonoBehaviour
    {
        public event Action<RenderTexture, RenderTexture> OnImageRendered;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            OnImageRendered?.Invoke(src, dest);
        }

        private void OnDestroy()
        {
            OnImageRendered = null;
        }
    }
}