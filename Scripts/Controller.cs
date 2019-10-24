using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HexagonSacit
{
    /// <summary>
    /// Controls the game and holds map data
    /// </summary>
    public class Controller : MonoBehaviour
    {
        private const float DISTANCE_SELECT_TILE = 1;
        private const string TIMER_SIMULATION = "TIMER_SIMULATION";
        private const float DURATION_ROTATION_STEP = 0.150f;

        public Tile tilePrototype;
        public int countTilesHorizontal = 4;
        public int countTilesVertical = 4;
        private Tile mouseOver;
        public Trio trioSelected;
        private System.Random random = new System.Random();
        private GameState gameState = GameState.FREE;
        public TimerVault timerVault = new TimerVault();
        public AccurateTimer timerSimulation;
        private int rotationDirection = 1;

        /// <summary>
        /// Determines which the game is in
        /// </summary>
        public enum GameState
        {
            FREE,
            SELECTION,
            ROTATION
        }

        void Start()
        {
            weaveTiles(new Vector2(0, 0), countTilesHorizontal, countTilesVertical);

            timerSimulation = AccurateTimer.createWithoutLimit().freeze(false);
            timerVault.add(TIMER_SIMULATION, timerSimulation);
        }


        void Update()
        {
            switch (gameState)
            {
                case GameState.ROTATION:
                    maintainRotation();
                    break;

                case GameState.SELECTION:
                    checkForRotationInput();
                    break;
            }
        }

        private void LateUpdate()
        {
            timerVault.update();
        }

        private void checkForRotationInput()
        {
            if (trioSelected != null)
            {
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    startRotation(rotationDirection = -1);
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow) && trioSelected != null)
                {
                    startRotation(rotationDirection = 1);
                }
            }
        }

        private void checkForMatchingTiles()
        {
            //check for matches
            HashSet<Tile> tilesMatching = trioSelected.checkForMatchingTiles();

            if (tilesMatching != null)
            {
                foreach (Tile tileMatching in tilesMatching)
                {
                    tileMatching.replace();
                }

                gameState = GameState.FREE;
                return;
            }
        }

        /// <summary>
        /// Maintains the rotation of selected trio, rotating it and checking for matches at every step.
        /// </summary>
        private void maintainRotation()
        {
            if (timerSimulation.isAfter(DURATION_ROTATION_STEP))
            {
                trioSelected.rotate(rotationDirection);

                //Rotation finished
                if (trioSelected.rotationCycle == 0)
                {
                    gameState = GameState.SELECTION;
                }

                //Check for matching tiles
                checkForMatchingTiles();

                timerSimulation.reset();
            }
        }

        private void startRotation(int direction)
        {
            timerSimulation.reset();
            gameState = GameState.ROTATION;
        }

        /// <summary>
        /// Called to update the tile which the mouse is over.
        /// </summary>
        public void updateHighlightedTile(Tile tile)
        {
            mouseOver = tile;
        }

        /// <summary>
        /// Zooms the given trio either in or out.
        /// </summary>
        /// <param name="trio"></param>
        /// <param name="option">-1 in, 1 out</param>
        private void zoomTrio(Trio trio, int option)
        {
            foreach (Tile tile in trio.tiles)
            {
                tile.transform.Translate(0, 0, DISTANCE_SELECT_TILE * option);
            }
        }

        /// <summary>
        /// Selects the most suitable trio
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public Trio selectTrio(Tile tile)
        {
            //Determine the other tiles depending on the direction of the click.
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Constants.CAMERA_Z));
            float angleClick = Geometry.pointDirection(tile.transform.position, mousePos);

            int closestVertex = Geometry.closestVertexFromAngle(angleClick);

            Trio trio = Trio.create(tile, closestVertex);

            if (trio != null)
            {
                if (trioSelected != null)
                    zoomTrio(trioSelected, 1);

                trioSelected = trio;
                zoomTrio(trio, -1);

                gameState = GameState.SELECTION;
            }

            return null;
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
                    tile.name = Constants.NAME_PREFIX_TILE + "even (" + row + column + ")";
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
                    tile.name = Constants.NAME_PREFIX_TILE + "odd (" + row + column + ")";
                }
            }
        }

        /// <summary>
        /// Returns one of the predefined tile colors.
        /// </summary>
        /// <returns></returns>
        public Color randomTileColor()
        {
            return Constants.TILE_COLORS[random.Next(0, 4)];
        }
    }
}

