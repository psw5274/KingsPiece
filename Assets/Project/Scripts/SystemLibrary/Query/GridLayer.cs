using System;
using UnityEngine;

namespace Coordination.Query
{
    [CreateAssetMenu(fileName = "New Grid Layer", menuName = "Query Layer/Grid", order = 0)]
    public class GridLayer : QueryLayer
    {
        [Serializable]
        public class Repeater
        {
            [SerializeField]
            [Range(1, 7)]
            private int count = 1;
            [SerializeField]
            private Vector2Int delta;
        }

        [SerializeField]
        private CoordinationGrid gridMask;
        [SerializeField]
        private Repeater repeater;

        public override CoordinationGrid Query(CoordinationGrid coordinations)
        {
            throw new System.NotImplementedException();
        }
    }
}