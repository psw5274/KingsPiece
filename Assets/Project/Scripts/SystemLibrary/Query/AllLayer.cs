namespace Coordination.Query
{
    public class AllLayer : QueryLayer
    {
        public CoordinationGrid Query()
        {
            throw new System.NotImplementedException();
        }

        public override CoordinationGrid Query(CoordinationGrid coordinations)
        {
            return Query();
        }
    }
}