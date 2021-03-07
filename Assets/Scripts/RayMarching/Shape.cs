using System.Runtime.InteropServices;
using UnityEngine;

namespace Raymarching
{
    public readonly struct Shape
    {
        public readonly Vector3 Position, Color, Size;
        public readonly float Radius;
        public readonly Operation Operation;
        public readonly ShapeType Type;
        public readonly float BlendStrength;
        public readonly int ChildCount;

        public Shape(Vector3 position,
            Vector3 color,
            Vector3 size,
            float radius,
            Operation operation,
            ShapeType type,
            float blendStrength,
            int childCount)
        {
            Position = position;
            Color = color;
            Size = size;
            Radius = radius;
            Operation = operation;
            Type = type;
            BlendStrength = blendStrength;
            ChildCount = childCount;
        }

        public static int SizeOf() => Marshal.SizeOf<Shape>();
    }

    public enum Operation
    {
        None = 0,
        Cut = 1,
        Mask = 2,
        Blend = 3
    }

    public enum ShapeType
    {
        Sphere = 0,
        Box = 1,
        Octahedron = 2,
        Plane = 3,
        Torus = 4,
    }
}