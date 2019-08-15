using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill", order = 0)]
public class Skill : ScriptableObject
{
    public enum ApplyingTiming { Instant, Attack, Damaged, Moved, TurnPassed }
    [Serializable]
    public class Parameter
    {
        public enum ModifyTarget
        {
            HPAddition,
            HPMultiplication,
            ATKAddition,
            ATKMultiplication,
            Movability,
            Unbeatability,
            HPHeal,
            HPDamage
        }

        public ModifyTarget target;
        public int value;
    }

    public string label;
    public int duration;
    public ApplyingTiming timing;
    public Parameter[] parameters;
    public bool isTargetOpposite;
    public GridQuery relativeTargetGrid = new GridQuery();

    public List<BoardCoord> GetAvailableTargetCoord()
    {
        BoardCoord center = BoardManager.Instance.isSkillReady ? BoardManager.Instance.selectedPiece.GetComponent<Piece>().pieceCoord : BoardCoord.NEGATIVE;
        TeamColor targetTeamColor;

        if (isTargetOpposite)
        {
            if (GameManager.Instance.currentTurn == TeamColor.White)
            {
                targetTeamColor = TeamColor.Black;
            }
            else
            {
                targetTeamColor = TeamColor.White;
            }
        }
        else
        {
            if (GameManager.Instance.currentTurn == TeamColor.White)
            {
                targetTeamColor = TeamColor.White;
            }
            else
            {
                targetTeamColor = TeamColor.Black;
            }
        }

        return relativeTargetGrid.TargetCoordinationQuery(center, targetTeamColor).ToList();
    }

    public void Operate(BoardCoord selectedBoardCoord)
    {
        var target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();
        EffectManager.Instance.AddEffect(target, this);
    }
}
