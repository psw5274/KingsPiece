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
    }
}
