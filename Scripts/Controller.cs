using UnityEngine;
using System.Collections;

namespace HexagonSacit
{
    /// <summary>
    /// Controls the game and holds map data
    /// </summary>
    public class Controller : MonoBehaviour
    {
        

        public Tile tilePrototype;

        void Start()
        {
            weaveTiles(new Vector2(0, 0), 8, 9);
        }


        void Update()
        {

        }

        /// <summary>
        /// Weaves tiles according to the given parameters.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="countHorizontal"></param>
        /// <param name="countVertical"></param>
        private void weaveTiles(Vector2 startingPoint, int countHorizontal, int countVertical)
        {
            float x, y;
           
            //Weave even y tiles
            int countColumnEven = Mathf.CeilToInt(countHorizontal / 2);

            for (int row = 0; row < countVertical; row++)
            {
                y = startingPoint.y + (row * Constants.LENGTH_SIDE_TO_SIDE);

                for (int column = 0; column < countColumnEven; column++)
                {
                    x = startingPoint.x + (column * (Constants.LENGTH_EDGE + Constants.LENGTH_VERTEX_TO_VERTEX));

                    Instantiate(tilePrototype, new Vector3(x, y, tilePrototype.transform.position.z), tilePrototype.transform.rotation);
                }
            }

            //Weave odd y tiles
            int countColumnOdd = Mathf.FloorToInt(countHorizontal / 2);

            for (int row = 0; row < countVertical - 1; row++)
            {
                y = (startingPoint.y + Constants.LENGTH_SIDE_TO_SIDE / 2) + (Constants.LENGTH_SIDE_TO_SIDE * row);

                for (int column = 0; column < countColumnOdd; column++)
                {
                    x = (startingPoint.x + (Constants.LENGTH_VERTEX_TO_VERTEX + Constants.LENGTH_EDGE) / 2) + 
                        ((Constants.LENGTH_EDGE + Constants.LENGTH_VERTEX_TO_VERTEX) * column);

                    Instantiate(tilePrototype, new Vector3(x, y, tilePrototype.transform.position.z), tilePrototype.transform.rotation);
                }
            }
        }
    }
}

