
using System.Collections.Generic;

namespace SkillSystem
{
    public abstract class MagicSkill : BaseSkill
    {
        public abstract List<BoardCoord> GetTargetCoordinations();
        public abstract void Operate(BoardCoord[] targets);
    }
}