using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill00003", menuName = "Piece Skill Card/Skill00003", order = 6)]
    public class Skill00003 : PieceSkill
    {
        public int damage = 0;

        public override void Operate(Piece self, BoardCoord[] targets)
        {
            /*
             * 설명
             * 주변의 모든 방향 1칸에 있는 적들에게 5의 피해를 입힌다.
             */

            Piece[] pieces = GetTargets(self);
            BoardCoord[] positions = GetPositions(self);

            OperateBegin(self, positions);
            ForEachPiece(pieces, piece => DamageHP(piece, damage));
            OperateEnd(self, positions);
        }
    }
}