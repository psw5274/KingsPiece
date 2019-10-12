using System.Collections.Generic;
using System.Linq;
using BoardSystem;
using PieceSystem;

namespace SkillSystem
{
    public abstract class PieceSkill : BaseSkill
    {
        public BoardLayer layer;

        public void ForEachPiece(Piece[] pieces, System.Action<Piece> action)
        {
            for (int i = 0; i < pieces.Count(); ++i)
            {
                action(pieces[i]);
            }
        }

        public BoardCoord[] GetPositions(Piece self)
        {
            return GetTargetCoordinations(self).ToArray();
        }

        public virtual List<BoardCoord> GetTargetCoordinations(Piece self)
        {
            List<BoardCoord> coords = new List<BoardCoord>();

            coords = layer.GetCoordinations(self).ToList();

            return coords;
        }

        public Piece[] GetTargets(Piece self)
        {
            System.Func<BoardCoord, Piece> selecter;
            selecter = coord => BoardManager.Instance.GetPieceAt(coord);

            return GetTargetCoordinations(self).Select<BoardCoord, Piece>(selecter)
                                               .ToArray();
        }

        public abstract void Operate(Piece self, BoardCoord[] targets);

        public void OperateBegin(Piece self, BoardCoord[] targets)
        {

        }

        public void OperateEnd(Piece self, BoardCoord[] targets)
        {
            self.UpdateStatus();

            foreach (var target in targets)
            {
                BoardManager.Instance.GetPieceAt(target).UpdateStatus();
            }
        }
    }
}
