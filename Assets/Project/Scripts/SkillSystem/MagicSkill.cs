using UnityEngine;

namespace SkillSystem
{
    public abstract class MagicSkill : ScriptableObject
    {
        public abstract void Operate(BoardCoord[] targets);
    }
}