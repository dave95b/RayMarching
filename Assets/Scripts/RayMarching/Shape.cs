using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Raymarching
{
    public readonly struct Shape
    {
        public readonly Vector3 Position, Color;
        public readonly float Radius;
        public readonly Operation Operation;
        public readonly float BlendStrength;

        public Shape(Vector3 position, Vector3 color, float radius, Operation operation, float blendStrength)
        {
            Position = position;
            Color = color;
            Radius = radius;
            Operation = operation;
            BlendStrength = blendStrength;
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
}