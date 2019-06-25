using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSkill", menuName = "Skill/AttackSkill")]
public class AttackSkill : Skill
{
    public override List<BoardCoord> GetAvailableTargetCoord()
    {
        List<BoardCoord> targetCoordList = new List<BoardCoord>();

        for(int i = 0; i < BoardManager.NUM_BOARD_COL; i++)
        {
            for(int j = 0; j < BoardManager.NUM_BOARD_ROW; j++)
            {
                GameObject tmpObject = BoardManager.Instance.boardStatus[i][j];
                if (tmpObject != null)
                {
                    targetCoordList.Add(tmpObject.GetComponent<Piece>().pieceCoord);
                }
            }
        }
        return targetCoordList;
    }

    public override void SkillScript(params BoardCoord[] targetCoords)
    {
        Piece targetPiece;
        foreach (BoardCoord c in targetCoords)
        {
            targetPiece = BoardManager.Instance.boardStatus[c.col][c.row].GetComponent<Piece>();
            targetPiece.statHP -= param[0];
            targetPiece.UpdateStatus();
            Debug.Log("DEAL " + param[0] + " TO " + c.col + ", " + c.row);
        }
    }
}
