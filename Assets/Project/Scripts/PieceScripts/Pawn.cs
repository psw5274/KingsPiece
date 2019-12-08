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

        // 폰은 직진이라 방향 잡아줘야함
        int direction = (int)teamColor * (int)PlayerManager.Instance.GetPlayerTeamColor();

        tmpCoord = pieceCoord + new BoardCoord(0, 1) * direction;
        GameObject targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];
        if (tmpCoord.IsAvailable() && targetCoordObject == null)
        {
            moveDestinationList.Add(tmpCoord);

            if (!isMovedFirst)
            {
                tmpCoord = pieceCoord + new BoardCoord(0, 2) * direction;
                targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];

                if (targetCoordObject == null)
                    moveDestinationList.Add(tmpCoord);
            }
        }

        tmpCoord = pieceCoord + new BoardCoord(1, 1) * direction;
        if (tmpCoord.IsAvailable())
        {
            targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];
            if (targetCoordObject != null &&
               targetCoordObject.GetComponent<Piece>().teamColor != this.teamColor)
            {
                attackTargetList.Add(tmpCoord);
            }
        }
        tmpCoord = pieceCoord + new BoardCoord(-1, 1) * direction;
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

    public override bool Attack(BoardCoord targetCoord, bool isNetworkCommand = false)
    {
        Piece target = boardManager.boardStatus[targetCoord.col][targetCoord.row].GetComponent<Piece>();
        target.CurrentHP -= this.CurrentATK;
        target.UpdateStatus();

        if (!isNetworkCommand)
        {
            NetworkManager.Instance.SendingPacketEnqueue(new NetworkPacket(PacketType.ATTACK,
                            pieceCoord.col, pieceCoord.row, targetCoord.col, targetCoord.row));
        }

        // tmp effect
        var effect = Instantiate(this.pieceAttackEffect,
                        targetCoord.GetBoardCoardVector3() + new Vector3(0, 3.5f, 0.01f),
                        Quaternion.identity).transform.localScale *= 10.0f;
        var animator = GetComponent<Animator>();
        animator.Play("Attack", -1, 0f);

        if (target.pieceStatus == PieceStatus.Dead)
        {
            this.Move(targetCoord, true);
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
