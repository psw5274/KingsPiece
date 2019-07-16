using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Pawn : Piece
{

    public override void GetAvailableDestination()
    {
        moveDestinationList.Clear();
        attackTargetList.Clear();
        ResetBoardCoord();

        if (movableCount == 0)
        {
            return;
        }

        BoardCoord tmpCoord;

        tmpCoord = pieceCoord + new BoardCoord(0, 1) * (int)teamColor;
        GameObject targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];
        if (tmpCoord.IsAvailable() && targetCoordObject == null)
        {
            moveDestinationList.Add(tmpCoord);

            if (!isMovedFirst)
            {
                tmpCoord = pieceCoord + new BoardCoord(0, 2) * (int)teamColor;
                targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];

                if (targetCoordObject == null)
                    moveDestinationList.Add(tmpCoord);
            }
        }

        tmpCoord = pieceCoord + new BoardCoord(1, 1) * (int)teamColor;
        if (tmpCoord.IsAvailable())
        {
            targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];
            if (targetCoordObject != null &&
               targetCoordObject.GetComponent<Piece>().teamColor != this.teamColor)
            {
                attackTargetList.Add(tmpCoord);
            }
        }
        tmpCoord = pieceCoord + new BoardCoord(-1, 1) * (int)teamColor;
        if (tmpCoord.IsAvailable())
        { 
            targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];
            if (targetCoordObject != null &&
               targetCoordObject.GetComponent<Piece>().teamColor != this.teamColor)
            {
                attackTargetList.Add(tmpCoord);
            }
        }
    }

    public override bool Attack(BoardCoord targetCoord)
    {
        if (!attackTargetList.Exists(x => x == targetCoord))
            return false;

        Piece target = boardManager.boardStatus[targetCoord.col][targetCoord.row].GetComponent<Piece>();
        target.CurrentHP -= this.CurrentATK;
        target.UpdateStatus();

        if (target.pieceStatus == PieceStatus.Dead)
        {
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = null;
            this.pieceCoord = targetCoord;
            this.transform.position = pieceCoord.GetBoardCoardVector3();
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = this.gameObject;
        }

        EffectManager.Instance.NotifyAttacking(this);
        EffectManager.Instance.NotifyDamaged(target);
        return true;
    }

    public override bool UseSkill()
    {

        return true;
    }
}
