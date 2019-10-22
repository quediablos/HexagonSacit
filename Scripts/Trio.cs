using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{
    /// <summary>
    /// Represents a selection of trio of tiles
    /// </summary>
    public class Trio
    {
        private static int[] ORIENTATION_0_120_240 = { 0, 120, 240 };
        private static int[] ORIENTATION_60_180_300 = { 60, 180, 300 };

        public Tile[] tiles = new Tile[3];
        public int[] angles = new int[3]; //Angles of the tiles from the intersection. Either 0, 120, 240 or 60, 180, 300.

        public static Trio create(Tile tileSelected, int vertex)
        {
            Trio trio = new Trio();

            //Determine the angle orientation of tiles.
            if (vertex == 60 || vertex == 180 || vertex == 330)
                trio.angles = ORIENTATION_0_120_240;
            else
                trio.angles = ORIENTATION_60_180_300;

            Tile[] neighbors = tileSelected.getNeighborsAt(vertex);

            if (neighbors == null)
                return null;

            trio.tiles[0] = tileSelected;
            trio.tiles[1] = neighbors[0];
            trio.tiles[2] = neighbors[1];

            return trio;
        }
       
    }
}
