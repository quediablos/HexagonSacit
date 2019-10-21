using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{


    public static class Geometry
    {
        public const float EQUALITY_MARGIN_FOR_POINTS = 0.01f;


        public static Vector2 vector3ToVector2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 vector2Vector3(Vector2 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static float pointDirection(Vector2 p1, Vector2 p2)
        {
            return angle(Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI);
        }

        public static float pointDistance(Vector2 p1, Vector2 p2)
        {
            return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        }

        public static float angleDifference(Vector2 p1, Vector2 p2)
        {
            return Mathf.Atan2(p1.x * p2.y - p1.y * p2.x, p1.x * p2.x + p1.y * p2.y);
        }

        public static Vector2 angleToVector(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }


        public static float oppositeAngle(float angle)
        {
            return (angle + 180f) % 360f;
        }

        public static float angle(float val)
        {
            float angle = val % 360;

            if (val < 0)
                angle += 360;

            return angle;
        }

        public static bool almostAt(Vector2 p1, Vector2 p2, float distanceThreshold)
        {
            return pointDistance(p1, p2) <= distanceThreshold;
        }

        public static int comparePointsByDistance(Vector2 p1, Vector2 p2, Vector2 reference)
        {
            float dist1 = pointDistance(p1, reference);
            float dist2 = pointDistance(p2, reference);

            if (dist1 > dist2)
                return 1;
            else
                return -1;
        }


        public static Vector2 makeVector(float magnitude, float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude, Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude);
        }


    }
}