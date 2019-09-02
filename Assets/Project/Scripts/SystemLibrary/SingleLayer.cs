using System;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Single Layer", menuName = "Board Layer/Single", order = 5)]
    [Serializable]
    public class SingleLayer : BoardLayer
    {
        public BoardCoord coordination;

        public override BoardCoord[] GetCoordinations()
        {
            return new BoardCoord[] { coordination };
        }
    }
}
