using UnityEngine;

namespace Coordination.Query
{
    public abstract class QueryLayer : ScriptableObject
    {
        public abstract CoordinationGrid Query(CoordinationGrid coordinations);
    }
}