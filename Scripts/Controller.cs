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
        public int countTilesHorizontal = 4;
        public int countTilesVertical = 4;
        private Tile mouseOver;
        private Tile tileSelected;
        private System.Random random = new System.Random();
        private const float DISTANCE_SELECT_TILE = 1;

        void Start()
        {
            weaveTiles(new Vector2(0, 0), countTilesHorizontal, countTilesVertical);
            
        }


        void Update()
        {

        }

        /// <summary>
        /// Called to update the tile which the mouse is over.
        /// </summary>
        public void updateHighlightedTile(Tile tile)
        {
            mouseOver = tile;
        }

        /// <summary>
        /// Selects the given tile
        /// </summary>
        /// <param name="tile"></param>
        public void selectTile(Tile tile)
        {
            //move the old previously selected tile backward.
            if (tileSelected != null)
                tileSelected.transform.Translate(0, 0, DISTANCE_SELECT_TILE);

            //move the new selected tile forward.
            tileSelected = tile;
            tileSelected.transform.Translate(0, 0, -DISTANCE_SELECT_TILE);
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

            for (int row = 0; row < countVertical -1; row++)
            {
                y = startingPoint.y + (row * Constants.LENGTH_SIDE_TO_SIDE);

                for (int column = 0; column < countColumnEven; column++)
                {
                    x = startingPoint.x + (column * (Constants.LENGTH_EDGE + Constants.LENGTH_VERTEX_TO_VERTEX));

                    Tile tile = Instantiate(tilePrototype, new Vector3(x, y, tilePrototype.transform.position.z), tilePrototype.transform.rotation);
                    tile.color = randomTileColor();
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

                    Tile tile = Instantiate(tilePrototype, new Vector3(x, y, tilePrototype.transform.position.z), tilePrototype.transform.rotation);
                    tile.color = randomTileColor();
                }
            }
        }

        /// <summary>
        /// Returns one of the predefined tile colors.
        /// </summary>
        /// <returns></returns>
        private Color randomTileColor()
        {
            return Constants.TILE_COLORS[random.Next(0, 4)];
        }
    }
}

