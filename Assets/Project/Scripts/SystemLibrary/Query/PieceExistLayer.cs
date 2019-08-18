using UnityEngine;

namespace Coordination.Query
{
    [CreateAssetMenu(fileName = "New Piece Exist Layer", menuName = "Query Layer/Piece Exist", order = 0)]
    public class PieceExistLayer : QueryLayer
    {
        [SerializeField]
        public bool findPieceExistCoordination;

        public override CoordinationGrid Query(CoordinationGrid coordinations)
        {
            throw new System.NotImplementedException();
        }
    }
}