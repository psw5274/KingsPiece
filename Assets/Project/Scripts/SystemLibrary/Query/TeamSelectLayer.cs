using UnityEngine;

namespace Coordination.Query
{
    [CreateAssetMenu(fileName = "New Team Select Layer", menuName = "Query Layer/Team Select", order = 0)]
    public class TeamSelectLayer : QueryLayer
    {
        public enum RelativeTeam
        {
            Self,
            Opposite
        }

        [SerializeField]
        private RelativeTeam targetTeam;

        public override CoordinationGrid Query(CoordinationGrid coordinations)
        {
            throw new System.NotImplementedException();
        }
    }
}