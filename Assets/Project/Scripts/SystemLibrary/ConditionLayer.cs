using System.Linq;
using BoardSystem.Query;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Condition Layer", menuName = "Board Layer/Condition", order = 6)]
    public class ConditionLayer : BoardLayer
    {
        public BoardLayer layer;
        public Condition conditions;

        public override BoardCoord[] GetCoordinations()
        {
            return layer.GetCoordinations()
                        .Where(coordination => conditions.Check(coordination))
                        .ToArray();
        }
    }
}