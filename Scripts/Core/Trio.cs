using UnityEngine;
using System.Collections.Generic;


namespace HexagonSacit
{
    /// <summary>
    /// Represents a selection of trio of tiles
    /// </summary>
    public class Trio
    {
        private static int[] ORIENTATION_0_120_240 = { 0, 120, 240 };
        private static int[] ORIENTATION_60_180_300 = { 60, 180, 300 };
        public int rotationCycle = 0; //0->1->2->0

        public Tile[] tiles = new Tile[3];
        public int[] angles = new int[3]; //Angles of the tiles from the intersection. Either 0, 120, 240 or 60, 180, 300.

        /// <summary>
        /// Creates a trio based on given parameters.
        /// </summary>
        /// <param name="tileSelected"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static Trio create(Tile tileSelected, int vertex)
        {
            Trio trio = new Trio();
            
            Tile[] neighbors = tileSelected.getNeighborsAt(vertex);

            if (neighbors == null)
                return null;

            Tile tileA = tileSelected;
            Tile tileB = neighbors[0];
            Tile tileC = neighbors[1];

            //Determine the positions of tiles.
            if (vertex == 60)
            {
                trio.tiles[0] = tileSelected.getNeighborAt(30);
                trio.tiles[1] = tileSelected.getNeighborAt(90);
                trio.tiles[2] = tileSelected;

                trio.angles = ORIENTATION_0_120_240;
            }
            else if (vertex == 180)
            {
                trio.tiles[0] = tileSelected;
                trio.tiles[1] = tileSelected.getNeighborAt(150);
                trio.tiles[2] = tileSelected.getNeighborAt(210);

                trio.angles = ORIENTATION_0_120_240;
            }
            else if (vertex == 300)
            {
                trio.tiles[0] = tileSelected.getNeighborAt(330);
                trio.tiles[1] = tileSelected;
                trio.tiles[2] = tileSelected.getNeighborAt(270);

                trio.angles = ORIENTATION_0_120_240;
            }
            else if(vertex == 0)
            {
                trio.tiles[0] = tileSelected.getNeighborAt(30);
                trio.tiles[1] = tileSelected;
                trio.tiles[2] = tileSelected.getNeighborAt(330);

                trio.angles = ORIENTATION_60_180_300;
            }
            else if (vertex == 120)
            {
                trio.tiles[0] = tileSelected.getNeighborAt(90);
                trio.tiles[1] = tileSelected.getNeighborAt(150);
                trio.tiles[2] = tileSelected;

                trio.angles = ORIENTATION_60_180_300;
            }
            else if(vertex == 240)
            {
                trio.tiles[0] = tileSelected;
                trio.tiles[1] = tileSelected.getNeighborAt(210);
                trio.tiles[2] = tileSelected.getNeighborAt(270);

                trio.angles = ORIENTATION_60_180_300;
            }
            

            return trio;
        }

        /// <summary>
        /// Rotates the trio one step.
        /// </summary>
        /// <param name="direction">1 for counter clockwise, -1 for clockwise</param>
        public void rotate(int direction)
        {
            rotationCycle = rotationCycle == 2 ? 0 : rotationCycle + 1;

            Vector3 pos0 = tiles[0].transform.position;
            Vector3 pos1 = tiles[1].transform.position;
            Vector3 pos2 = tiles[2].transform.position;

            Tile tile0 = tiles[0];
            Tile tile1 = tiles[1];
            Tile tile2 = tiles[2];

            if (direction == 1)
            {
                tiles[0].transform.position = pos1;
                tiles[1].transform.position = pos2;
                tiles[2].transform.position = pos0;

                tiles[0] = tile2;
                tiles[2] = tile1;
                tiles[1] = tile0;

            }
            else
            {
                tiles[0].transform.position = pos2;
                tiles[1].transform.position = pos0;
                tiles[2].transform.position = pos1;

                tiles[0] = tile1;
                tiles[2] = tile0;
                tiles[1] = tile2;
            }

            //Update neigbours for each tile in the perimeter.
            HashSet<Tile> tilesInPerimeter = getTilesInPerimeter();
            foreach (Tile tile in tilesInPerimeter)
            {
                tile.findNeighbors();
            }
        }

        /// <summary>
        /// Returns all the tiles around the trio.
        /// </summary>
        /// <returns></returns>
        public HashSet<Tile> getTilesInPerimeter()
        {
            HashSet<Tile> tilesInPerimeter = new HashSet<Tile>();

            foreach (Tile tile in tiles)
            {
                Tile[] neighbors = tile.neighbors;
                foreach (Tile tileNeighbor in neighbors)
                {
                    if (tileNeighbor != null)
                        tilesInPerimeter.Add(tileNeighbor);
                }
            }

            return tilesInPerimeter;
        }

        public HashSet<Tile> checkForMatchingTiles()
        {
            HashSet<Tile> tilesAll = new HashSet<Tile>();
            
            foreach (Tile tile in tiles)
            {
                Tile[] tilesMatching = tile.checkForMatchingTiles();

                if (tilesMatching == null)
                    continue;

                foreach (Tile tileMatching in tilesMatching)
                {
                    tilesAll.Add(tileMatching);
                }
            }

            return tilesAll.Count > 0 ? tilesAll : null;
        }

    }
}
