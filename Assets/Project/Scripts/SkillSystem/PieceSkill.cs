using PieceSystem;

namespace SkillSystem
{
    public abstract class PieceSkill : BaseSkill
    {
        public abstract void Operate(Piece self, BoardCoord[] targets);
    }
}
