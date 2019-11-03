using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexagonSacit
{
    public class Tile : MonoBehaviour
    {

        public Tile[] neighbors; //Neighbor tiles at 30, 90, 150...
        public Color color;
        public Controller controller;
        public Renderer renderer;
        public GameObject bombInd;
        private bool isBomb = false;
        public int column;

        void Start()
        {
            renderer = GetComponent<Renderer>();

            findNeighbors();
        }


        void Update()
        {
  
        }

        private void LateUpdate()
        {
            renderer.material.color = color;
        }

        private void OnMouseOver()
        {
            controller.updateHighlightedTile(this);
        }
        

        /// <summary>
        /// Enables the explosion mechanism.
        /// </summary>
        /// <param name="enable"></param>
        public void enableBomb(bool enable)
        {
            isBomb = enable;
            bombInd.SetActive(enable);
        }

        /// <summary>
        /// Replaces the tile, renewing its color
        /// </summary>
        public void replace()
        {
            color = arrangeNewColor();

            //Deactivate bomb feature
            enableBomb(false);  
        }

        /// <summary>
        /// Arranges a new suitable color for the tile.
        /// </summary>
        /// <returns></returns>
        public Color arrangeNewColor()
        {
            List<Color> colorsOfNeighbors = new List<Color>(6);
            List<Color> colorsAvailable = Constants.TILE_COLORS.GetRange(0, controller.numberOfColors);

            //Pick a color that neigbors don't have.
            foreach (Tile neighbor in neighbors)
            {
                if (neighbor != null)
                    colorsOfNeighbors.Add(neighbor.color);
            }

            List<Color> diff = colorsAvailable.Except(colorsOfNeighbors).ToList();

            Color colorNew;

            if (diff.Count > 0)
                colorNew = diff[0];
            else
                colorNew = controller.randomTileColor();

            return colorNew;
        }

        /// <summary>
        /// Checks the perimeters and finds the neigbor tiles.
        /// </summary>
        public void findNeighbors()
        {
            neighbors = new Tile[6];
            for (int angle = 30, i = 0; angle <= 330; angle += 60)
            {
                Vector3 collisionLineStart = Geometry.vector3ToVector2(transform.position) +
                    Geometry.makeVector(Constants.LENGTH_SIDE_TO_SIDE / 2 - Constants.MARGIN_COLLIDER, angle);

                Tile neighbor = Collision.collisionFreeOnRayUntil(collisionLineStart, angle, Constants.LENGTH_SIDE_TO_SIDE, Constants.LAYER_TILE);

                neighbors[i++] = neighbor;
            }
        }

        /// <summary>
        /// Returns the neighbor tile at the given angle (edge).
        /// </summary>
        /// <param name="edge">Values can be 30, 90, 150, 210, 270, 330</param>
        /// <returns>Null if there is no any</returns>
        public Tile getNeighborAt(int edge)
        {
            int i = System.Array.FindIndex(Constants.EDGE_ANGLES, a => a == edge);

            if (i == -1)
                return null;
            else
                return neighbors[i];
        }

        /// <summary>
        /// Returns the nth neighbor tile towards the given edge
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Tile getNthNeighborAt(int edge, int count)
        {
            Tile neighbor = getNeighborAt(edge);

            if (count == 1 || neighbor == null)
                return neighbor;
            else
                return neighbor.getNthNeighborAt(edge, count - 1);
        }

        /// <summary>
        /// Returns the neighbor tiles which intersect at the given vertex.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public Tile[] getNeighborsAt(int vertex)
        {
            Tile[] neighbors = new Tile[2];

            neighbors[0] = getNeighborAt((int) Geometry.angle(vertex - 30));
            neighbors[1] = getNeighborAt((int)Geometry.angle(vertex + 30));

            if (neighbors[0] == null || neighbors[1] == null)
                return null;
            else
                return neighbors;
        }

        /// <summary>
        /// Checks for tiles as a trio with the same color.
        /// </summary>
        /// <returns></returns>
        public Tile[] checkForMatchingTiles()
        {
            foreach (int vertex in Constants.VERTEX_ANGLES)
            {
                Tile[] neighbors = getNeighborsAt(vertex);

                if (neighbors == null)
                    continue;

                if (color.Equals(neighbors[0].color) && color.Equals(neighbors[1].color))
                {
                    Tile[] tilesMatching = new Tile[3];
                    tilesMatching[0] = this;
                    tilesMatching[1] = neighbors[0];
                    tilesMatching[2] = neighbors[1];

                    return tilesMatching;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if there is any possible match involving the tile itself.
        /// </summary>
        /// <returns></returns>
        public bool checkForPossibleMatchesAround()
        {
            //For each neighbor...
            foreach (int edge in Constants.EDGE_ANGLES)
            {
                Tile neighbor = getNeighborAt(edge);

                if (neighbor == null)
                    continue;

                //The neighbor's color should be the same.
                if (!neighbor.color.Equals(color))
                    continue;

                //Now for each common neighbor tile, check if the common neighbor itself or any of 
                //its neighbors except this tile and its neighbor is the same color.

                //Find common neighbors with the neighbor 
                Tile neighborCommon0 = getNeighborAt((int)Geometry.angle(edge - 60));
                Tile neighborCommon1 = getNeighborAt((int)Geometry.angle(edge + 60));

                //One of the common neighbors is the same color.
                if ((neighborCommon0 != null && neighborCommon0.color.Equals(color)) ||
                    (neighborCommon1 != null && neighborCommon1.color.Equals(color)))
                {
                    return true;
                }
                
                if (neighborCommon0 != null)
                {
                    //For each neighbor of common neighbor 0...
                    foreach (Tile neighborOfNeighborCommon0 in neighborCommon0.neighbors)
                    {
                        if (neighborOfNeighborCommon0 != null &&
                            !neighborOfNeighborCommon0.Equals(this) &&
                            !neighborOfNeighborCommon0.Equals(neighbor) &&
                            neighborOfNeighborCommon0.color.Equals(color))
                        {
                            return true;
                        }
                    }
                }
                
                if (neighborCommon1 != null)
                {
                    //For each neighbor of common neighbor 1...
                    foreach (Tile neighborOfNeighborCommon1 in neighborCommon1.neighbors)
                    {
                        if (neighborOfNeighborCommon1 != null &&
                            !neighborOfNeighborCommon1.Equals(this) &&
                            !neighborOfNeighborCommon1.Equals(neighbor) &&
                            neighborOfNeighborCommon1.color.Equals(color))
                        {
                            return true;
                        }
                    }
                }   
            }

            return false;
        }

    }
}