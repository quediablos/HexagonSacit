using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{
    public class Constants
    {
        public static float LENGTH_SIDE_TO_SIDE = 2f;
        public static float LENGTH_VERTEX_TO_VERTEX = 2.3f;
        public static float LENGTH_SPACE_BETWEEN_TILES = 0.05f;
        public static float LENGTH_EDGE = 1.155f;
        public static float MARGIN_COLLIDER = 0.05f;

        public static Color[] TILE_COLORS = { new Color(1, 0, 0), new Color(0, 1, 0), new Color(0, 0, 1), new Color(0, 1, 1) };
        public static int[] EDGE_ANGLES = { 30, 90, 150, 210, 270, 330 };
        public static int[] VERTEX_ANGLES = { 0, 60, 120, 180, 240, 300 };

        public static string LAYER_TILE = "LAYER_TILE";

        public static float CAMERA_Z = -10;

        public static string NAME_PREFIX_TILE = "tile_";
    }
}

