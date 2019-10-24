using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexagonSacit
{
    public class Tile : MonoBehaviour
    {
       

        public Tile[] neighbors; //Neighbor tiles at 30, 90, 150...
        public Color color;
        public Controller controller;
        private Renderer renderer;

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

        private void OnMouseDown()
        {
            controller.selectTrio(this);
        }

        /// <summary>
        /// Replaces the tile, renewing its color
        /// </summary>
        public void replace()
        {
            color = controller.randomTileColor();
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
                    return null;

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

    }
}