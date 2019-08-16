using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Skill With Team Color Option", menuName = "Skill Adapter/Team Color Option", order = 0)]
public class SkillAdapterTeamColorOption : Skill
{
    [SerializeField]
    private Skill skillForOppositeTeam;

    public override void Operate(BoardCoord selectedBoardCoord)
    {
        Piece target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();
        if (target.teamColor == GameManager.Instance.currentTurn)
        {
            base.Operate(selectedBoardCoord);
        }
        else
        {
            skillForOppositeTeam.Operate(selectedBoardCoord);
        }
    }
}