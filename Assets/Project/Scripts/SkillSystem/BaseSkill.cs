using System.Linq;
using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    public class BaseSkill : ScriptableObject
    {
        public Piece GetPieceAt(BoardCoord position)
        {
            return BoardManager.Instance.GetPieceAt(position);
        }
        public Piece GetCurrentKing()
        {
            if (GameManager.Instance.currentTurn == TeamColor.Black)
            {
                return BoardManager.Instance.kingBlack;
            }
            else
            {
                return BoardManager.Instance.kingWhite;
            }
        }
        public int GetDistanceBetween(Piece from, Piece to)
        {
            return BoardCoord.Distance(from.GetPosition(), to.GetPosition());
        }
        public BoardCoord GetDirection(Piece self, Piece target)
        {
            return (target.GetPosition() - self.GetPosition()).GetDirectionalCoord();
        }

        public int GetPieceATK(Piece piece)
        {
            return piece.GetCurrentATK();
        }

        public void DamageHP(Piece piece, int amount)
        {
            piece.DamageHP(amount);
        }
    }
}
