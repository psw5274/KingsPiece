using UnityEngine;

namespace SkillSystem
{
    public abstract class AffactationModule : ScriptableObject
    {
        public int priority;
        public abstract void Apply(Piece target, AffactationPriorityQueue queue);
    }
}