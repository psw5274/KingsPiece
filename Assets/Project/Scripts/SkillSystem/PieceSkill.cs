using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    public abstract class PieceSkill : ScriptableObject
    {
        public abstract void Operate(Piece self, BoardCoord[] targets);
    }
}
