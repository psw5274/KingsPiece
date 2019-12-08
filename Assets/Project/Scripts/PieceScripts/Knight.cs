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

        if (movableCount == 0)
        {
            return;
        }

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
}
