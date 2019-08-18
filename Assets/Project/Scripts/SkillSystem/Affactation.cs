using UnityEngine;

namespace SkillSystem
{
    public abstract class Affactation : ScriptableObject
    {
        public abstract void Operate(AffactationPriorityQueue queue);
    }
}