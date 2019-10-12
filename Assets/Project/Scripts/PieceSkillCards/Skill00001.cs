using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill00001", menuName = "Piece Skill Card/Skill00001", order = 6)]
    public class Skill00001 : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            /*
             * 설명
             * 십자가 방향 중 한 방향에 있는 적에게 거리에 따라서 다른 피해를 입힌다.
             * |거리|피해 배율|
             * |=1  |x3      |
             * |=2  |x2      |
             * |>2  |x1      |
             */

            Piece target = GetPieceAt(targets[0]);
            int damage = 4 - GetDistanceBetween(self, target);
            damage = damage < 1 ? 1 : damage;
            damage *= GetPieceATK(self);

            OperateBegin(self, targets);
            DamageHP(target, damage);
            OperateEnd(self, targets);
        }
    }
}