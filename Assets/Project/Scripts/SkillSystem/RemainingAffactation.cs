using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "New Remaining Affactation", menuName = "Skill/Affactation/Remaining", order = 0)]
    public class RemainingAffactation : Affactation
    {
        public override void Operate(AffactationPriorityQueue queue)
        {
            throw new System.NotImplementedException();
            _ = queue.target;
        }
    }
}