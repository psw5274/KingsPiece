using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill", order = 0)]
public class Skill : ScriptableObject
{
    public enum ApplyingTiming 
    { 
        Instant, 
        Attack, 
        Damaged, 
        Moved, 
        TurnPassed,
        AnyAction
    }

    public enum ApplyingTeam
    {
        Self,
        Opposite,
        Both
    }

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
            HPDamage,
            ActorPositionModification
        }

        public ModifyTarget target;
        public int value;
    }

    public string label;
    public int duration;
    public ApplyingTiming timing;
    public Parameter[] parameters;
    public ApplyingTeam team;
    public GridQuery relativeTargetGrid = new GridQuery();

    public virtual List<BoardCoord> GetAvailableTargetCoord()
    {
        GameObject piece = BoardManager.Instance.selectedPiece;
        BoardCoord center = piece == null ? BoardCoord.NEGATIVE : piece.GetComponent<Piece>().pieceCoord;
        TeamColor targetTeamColor;

        switch (team)
        {
            case ApplyingTeam.Self:
                targetTeamColor = GameManager.Instance.currentTurn;
                break;
            case ApplyingTeam.Opposite:
                targetTeamColor = GameManager.Instance.currentTurn == TeamColor.White ? TeamColor.Black : TeamColor.White;
                break;
            default:
                targetTeamColor = TeamColor.Both;
                break;
        }

        return relativeTargetGrid.TargetCoordinationQuery(center, targetTeamColor).ToList();
    }

    public virtual void Operate(BoardCoord selectedBoardCoord)
    {
        GameObject gameobject = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row];
        Piece target = gameobject == null ? null : gameobject.GetComponent<Piece>();
        EffectManager.Instance.AddEffect(target, this);
    }
}
