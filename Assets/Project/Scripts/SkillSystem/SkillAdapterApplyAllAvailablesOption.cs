using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill With Apply All Availables Option", menuName = "Skill Adapter/Apply All Availables Option", order = 0)]
public class SkillAdapterApplyAllAvailablesOption : Skill
{
    private List<BoardCoord> savedBoardCoordinations;

    public override List<BoardCoord> GetAvailableTargetCoord()
    {
        savedBoardCoordinations = base.GetAvailableTargetCoord();
        return savedBoardCoordinations;
    }

    public override void Operate(BoardCoord selectedBoardCoord)
    {
        foreach (BoardCoord coordination in savedBoardCoordinations)
        {
            Piece target = BoardManager.Instance.boardStatus[coordination.col][coordination.row].GetComponent<Piece>();
            EffectManager.Instance.AddEffect(target, this);
        }
    }
}