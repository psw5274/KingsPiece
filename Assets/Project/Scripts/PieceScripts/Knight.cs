using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private static readonly BoardCoord[] destCoords = new BoardCoord[]
    {
        BoardCoord.RIGHT + BoardCoord.UP_RIGHT, BoardCoord.RIGHT + BoardCoord.DOWN_RIGHT,
        BoardCoord.LEFT + BoardCoord.UP_LEFT, BoardCoord.LEFT + BoardCoord.DOWN_LEFT,
        BoardCoord.UP + BoardCoord.UP_RIGHT, BoardCoord.UP + BoardCoord.UP_LEFT,
        BoardCoord.DOWN + BoardCoord.DOWN_RIGHT, BoardCoord.DOWN + BoardCoord.DOWN_LEFT
    };

    public override void GetAvailableDestination()
    {
        moveDestinationList.Clear();
        attackTargetList.Clear();
        ResetBoardCoord();

        foreach (BoardCoord coord in destCoords)
        {
            BoardCoord tmpCoord = pieceCoord + coord;
            if (tmpCoord.IsAvailable())
            {
                GameObject targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];

                if (targetCoordObject != null)
                {
                    if (targetCoordObject.GetComponent<Piece>().teamColor != this.teamColor)
                        attackTargetList.Add(tmpCoord);
                }
                else
                    moveDestinationList.Add(tmpCoord);
            }
        }
    }

    public override bool UseSkill()
    {

        return true;
    }

    public override bool Attack(BoardCoord targetCoord)
    {
        if (!attackTargetList.Exists(x => x == targetCoord))
        {
            return false;
        }
        Piece target = boardManager.boardStatus[targetCoord.col][targetCoord.row].GetComponent<Piece>();
        target.statHP -= this.statATK;
        target.UpdateStatus();

        if (target.pieceStatus == PieceStatus.Dead)
        {
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = null;
            this.pieceCoord = targetCoord;
            this.transform.position = pieceCoord.GetBoardCoardVector3();
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = this.gameObject;
        }

        return true;
    }
}
