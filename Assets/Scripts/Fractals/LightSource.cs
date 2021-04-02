using System;
using UnityEngine;

namespace Fractals
{
    public class LightSource : MonoBehaviour
    {
        public event Action<LightData> OnLightChanged;

        [SerializeField]
        private new Light light;

        private LightData currentData;

        private void Start()
        {
            DispatchNewData(CreateLightData());
        }

        private void Update()
        {
            var data = CreateLightData();
            if (currentData != data)
                DispatchNewData(data);
        }

        private void OnDestroy() => OnLightChanged = null;

        private void DispatchNewData(in LightData data)
        {
            currentData = data;
            OnLightChanged?.Invoke(currentData);
        }

        private LightData CreateLightData() => new LightData(light.intensity, light.color, light.transform.forward);
    }

    public readonly struct LightData : IEquatable<LightData>
    {
        public readonly float Intensity;
        public readonly Color Color;
        public readonly Vector3 Direction;

        public LightData(float intensity, Color color, Vector3 direction)
        {
            Intensity = intensity;
            Color = color;
            Direction = direction;
        }

        public override bool Equals(object obj) => obj is LightData data && Equals(data);

        public bool Equals(LightData other) => Intensity == other.Intensity &&
                   Color.Equals(other.Color) &&
                   Direction.Equals(other.Direction);

        public override int GetHashCode()
        {
            int hashCode = 1017839884;
            hashCode = hashCode * -1521134295 + Intensity.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(LightData left, LightData right) => left.Equals(right);

        public static bool operator !=(LightData left, LightData right) => !(left == right);
    }
}