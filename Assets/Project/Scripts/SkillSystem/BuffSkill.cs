using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSkill", menuName = "Skill/BuffSkill")]
public class BuffSkill : Skill
{
    public int heal;
    public int atk;

    public override List<BoardCoord> GetAvailableTargetCoord()
    {
        List<BoardCoord> targetCoordList = new List<BoardCoord>();

        for (int i = 0; i < BoardManager.NUM_BOARD_COL; i++)
        {
            for (int j = 0; j < BoardManager.NUM_BOARD_ROW; j++)
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
        //for(int i = 0; i <)
    }
    private void start()
    {
    }
}
