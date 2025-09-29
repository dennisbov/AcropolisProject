
using System;
using UnityEngine;

namespace ProjectAcropolis
{
    [Serializable]
    public struct SphericalCoordinates
    {
        public float Longtitude;
        public float Latitude;
        public float Height;
        public Vector3 SphereCenter;

        public SphericalCoordinates(float longitude, float latitude, float height, Vector3 sphereCenter)
        {
            this.Longtitude = longitude;
            this.Latitude = latitude;
            this.Height = height;
            this.SphereCenter = sphereCenter;
        }

        public Vector3 ToCartisian()
        {
            return SphericalToCartesian(this);
        }

        public static Vector3 SphericalToCartesian(SphericalCoordinates spherical)
        {
            Vector3 result = new Vector3();
            result.y =
            result.y = Mathf.Sin(spherical.Longtitude);
            result.x = Mathf.Cos(spherical.Longtitude) * Mathf.Cos(spherical.Latitude);
            result.z = Mathf.Cos(spherical.Longtitude) * Mathf.Sin(spherical.Latitude);
            return result * spherical.Height + spherical.SphereCenter;
        }

        public static SphericalCoordinates CartesianToSpherical(Vector3 cartesian)
        {
            return new SphericalCoordinates();
        }
    }
}
