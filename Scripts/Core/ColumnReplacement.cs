using UnityEngine;
using System.Collections.Generic;

namespace HexagonSacit
{
    public class ColumnReplacement
    {
        public List<Tile> columns;
        public List<Color> nextColors;
        public List<Vector3> originalPositions;
        public int numberOfMatchingTiles;
        public float distanceToDrop;
        
        public ColumnReplacement(List<Tile> columns, int numberOfMatchingColors)
        {
            this.columns = columns;
            this.numberOfMatchingTiles = numberOfMatchingColors;
            this.nextColors = new List<Color>(columns.Count);
            this.originalPositions = new List<Vector3>(columns.Count);

            //Calculate total distance to drop
            distanceToDrop = Constants.LENGTH_SIDE_TO_SIDE * numberOfMatchingColors;
        }
    }
}
