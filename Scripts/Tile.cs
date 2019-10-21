using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexagonSacit
{
    public class Tile : MonoBehaviour
    {
       

        public Tile[] neighbors; //Neighbor tiles at 30, 90, 150...
        private Color color;
        public bool init = false;

        void Start()
        {
            if (init)
            findNeighbors();
        }


        void Update()
        {

        }

        /// <summary>
        /// Creates neighbors
        /// </summary>
        private void createNeighbors()
        {

        }

        /// <summary>
        /// Checks the perimeters and finds the neigbor tiles.
        /// </summary>
        private void findNeighbors()
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
    }
}