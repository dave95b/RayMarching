using System.Runtime.InteropServices;
using UnityEngine;

namespace Raymarching
{
    public readonly struct Shape
    {
        public readonly Vector3 Position, Color;
        public readonly float Radius;

        public Shape(Vector3 position, Vector3 color, float radius)
        {
            Position = position;
            Color = color;
            Radius = radius;
        }

        public static int SizeOf() => Marshal.SizeOf<Shape>();
    }
}