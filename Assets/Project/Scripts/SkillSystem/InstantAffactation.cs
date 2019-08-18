using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "New Instant Affactation", menuName = "Skill/Affactation/Instant", order = 0)]
    public class InstantAffactation : Affactation
    {
        public AffactationModule[] modules;

        public override void Operate(AffactationPriorityQueue queue)
        {
            foreach (var module in modules)
            {
                queue.Push(module);
            }
        }
    }
}