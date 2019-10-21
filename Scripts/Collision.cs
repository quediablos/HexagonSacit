using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{

    public abstract class Collision
    {
        /// <summary>
        /// Flexible collision line.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="layer"></param>
        /// <returns>Tile</returns>
        public static Tile collisionFreeOnLineUntil(Vector2 p1, Vector2 p2, string layer)
        {
            RaycastHit2D hit;
            hit = Physics2D.Linecast(p1, p2, 1 << LayerMask.NameToLayer(layer));
            
            if (hit.collider != null)
            {
                return hit.collider.gameObject.GetComponent<Tile>();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Applies a collision ray according to the given values
        /// </summary>
        /// <param name="from"></param>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static Tile collisionFreeOnRayUntil(Vector2 from, float angle, float distance, string layer)
        {
            Vector2 until = from + Geometry.makeVector(distance, angle);

            return collisionFreeOnLineUntil(from, until, layer);
        }
    }
}