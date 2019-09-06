using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class MagicSkill : ScriptableObject
    {
        public abstract List<BoardCoord> GetTargetCoordinations();
        public abstract void Operate(BoardCoord[] targets);
    }
}