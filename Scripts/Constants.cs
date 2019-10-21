using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{
    public class Constants : ScriptableObject
    {
        public static float LENGTH_SIDE_TO_SIDE = 2f;
        public static float LENGTH_VERTEX_TO_VERTEX = 2.3f;
        public static float LENGTH_SPACE_BETWEEN_TILES = 0.05f;
        public static float LENGTH_EDGE = 1.155f;
        public static float MARGIN_COLLIDER = 0.05f;

        public static string LAYER_TILE = "LAYER_TILE";
    }
}

