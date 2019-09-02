using System.Linq;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Repeat Layer", menuName = "Board Layer/Repeat", order = 4)]
    public class RepeatLayer : BoardLayer
    {
        public BoardLayer layer;
        public BoardCoord delta;
        [Range(1, 7)]
        public int count;

        public override BoardCoord[] GetCoordinations()
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var coordination in layer.GetCoordinations())
            {
                for (int i = 0; i < count; ++i)
                {
                    coords.Append(coordination + (delta * i));
                }
            }

            return coords;
        }
    }
}