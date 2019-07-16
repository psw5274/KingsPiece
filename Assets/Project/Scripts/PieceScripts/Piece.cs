﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovingDirection { Straight, Diagonal, Both }
public enum PieceStatus { Normal, Dead }

public abstract class Piece : MonoBehaviour
{
    protected BoardManager boardManager;
    public BoardCoord pieceCoord;

    public BasicCardData cardData;
    public TeamColor teamColor;

    protected List<object> bufList = new List<object>();
    public List<BoardCoord> moveDestinationList = new List<BoardCoord>();
    public List<BoardCoord> attackTargetList = new List<BoardCoord>();

    public int statHP = 0;
    public int statATK = 0;

    private int currentHP = 0;
    private int additionalHP = 0;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
            currentHP = Mathf.Clamp(currentHP, 0, MaxHP);
        }
    }
    private int MaxHP
    {
        get
        {
            return statHP + additionalHP;
        }
    }
    public int AdditionHPDelta
    {
        set
        {
            additionalHP += value;
            currentHP += Mathf.Clamp(value, 0, value);
            currentHP = Mathf.Clamp(currentHP, 0, MaxHP);
        }
    }
    public int MultiplicationHPDelta
    {
        set
        {
            int additionalValue = (int)(statHP * (value / 100.0f));
            additionalHP += additionalValue;
            currentHP += Mathf.Clamp(additionalValue, 0, additionalValue);
            currentHP = Mathf.Clamp(currentHP, 0, MaxHP);
        }
    }
    private int currentATK = 0;
    private int additionalATK = 0;
    public int CurrentATK
    {
        get
        {
            return currentATK;
        }
        set
        {
            currentATK = value;
            currentATK = Mathf.Clamp(currentATK, 0, MaxATK);
        }
    }
    private int MaxATK
    {
        get
        {
            return statATK + additionalATK;
        }
    }
    public int AdditionATKDelta
    {
        set
        {
            additionalATK += value;
            currentATK += Mathf.Clamp(value, 0, value);
            currentATK = Mathf.Clamp(currentATK, 0, MaxATK);
        }
    }
    public int MultiplicationATKDelta
    {
        set
        {
            int additionalValue = (int)(statATK * (value / 100.0f));
            additionalATK += additionalValue;
            currentATK += Mathf.Clamp(additionalValue, 0, additionalValue);
            currentATK = Mathf.Clamp(currentATK, 0, MaxATK);
        }
    }
    public int movableCount = 1;
    public int unbeatableCount = 0;
    public PieceStatus pieceStatus = PieceStatus.Normal;

    protected int dataAttackCount = 0;
    protected int dataHitCount = 0;
    protected int dataKillCount = 0;
    protected int dataMoveCount = 0;

    protected bool isMovedFirst = false;

    protected bool isUseSkill = false;

    protected MovingDirection movingDirection = MovingDirection.Both;
    protected int movingDistance = 7;
    
    public virtual void Initialize(TeamColor teamColor, HeroCard heroCard)
    {
        this.teamColor = teamColor;
        this.cardData = heroCard;
        cardData = (HeroCard)ScriptableObject.CreateInstance(typeof(HeroCard));

        statHP = heroCard.statHP;
        statATK = heroCard.statATK;
        currentHP = heroCard.statHP;
        currentATK = heroCard.statATK;
    }

    public virtual bool IsDestination()
    {
        return moveDestinationList.Count == 0 ? false : true;
    }
    public virtual bool IsDetinationAvailable(BoardCoord destCoord)
    {
        return destCoord.IsAvailable() && moveDestinationList.Exists(x => x == destCoord);
    }
    public virtual bool IsAttackAvailable(BoardCoord attackCoord)
    {
        return attackCoord.IsAvailable() && attackTargetList.Exists(x => x == attackCoord);
    }

    
    // Todo : GetAvailableAttackTaget()과 분리
    public virtual void GetAvailableDestination()
    {
        moveDestinationList.Clear();
        attackTargetList.Clear();
        ResetBoardCoord();

        if (movableCount == 0)
        {
            return;
        }

        List<BoardCoord> tmpList = new List<BoardCoord>();

        if (movingDirection == MovingDirection.Straight ||
            movingDirection == MovingDirection.Both)
            tmpList.AddRange(new BoardCoord[] { BoardCoord.RIGHT, BoardCoord.LEFT,
                                                BoardCoord.DOWN, BoardCoord.UP });

        if (movingDirection == MovingDirection.Diagonal ||
            movingDirection == MovingDirection.Both)
            tmpList.AddRange(new BoardCoord[] { BoardCoord.UP_RIGHT, BoardCoord.DOWN_RIGHT,
                                                BoardCoord.DOWN_LEFT, BoardCoord.UP_LEFT });

        foreach (BoardCoord coord in tmpList)
        {
            for (int i = 1; i <= movingDistance; i++)
            {
                BoardCoord tmpCoord = pieceCoord + (coord * i);
                if (tmpCoord.IsAvailable())
                {
                    GameObject targetCoordObject = boardManager.boardStatus[tmpCoord.col][tmpCoord.row];

                    if (targetCoordObject != null)
                    {
                        if (targetCoordObject.GetComponent<Piece>().teamColor != this.teamColor)
                            attackTargetList.Add(tmpCoord);
                        break;
                    }
                    else
                        moveDestinationList.Add(tmpCoord);
                }
                else
                    break;
            }
        }
    }

    public virtual bool Attack(BoardCoord targetCoord)
    {
        if (!attackTargetList.Exists(x => x == targetCoord))
        {
            return false;
        }
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
        else // not dead
        {
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = null;
            this.pieceCoord = targetCoord - (targetCoord - this.pieceCoord).GetDirectionalCoord();
            this.transform.position = pieceCoord.GetBoardCoardVector3();
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = this.gameObject;
        }

        EffectManager.Instance.NotifyAttacking(this);
        EffectManager.Instance.NotifyDamaged(target);
        return true;
    }

    public virtual bool Move(BoardCoord destCoord)
    {
        if (moveDestinationList.Exists(x => x == destCoord))
        {
            // 성공
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = null;

            pieceCoord = destCoord;
            this.transform.position = pieceCoord.GetBoardCoardVector3();
            boardManager.boardStatus[pieceCoord.col][pieceCoord.row] = this.gameObject;


            if (!isMovedFirst)
                isMovedFirst = true;

            EffectManager.Instance.NotifyMoved(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    public abstract bool UseSkill();

    public void UpdateStatus()
    {
        if (CurrentHP <= 0)
        {
            this.pieceStatus = PieceStatus.Dead;
            this.Die();
        }
    }

    protected virtual void Die()
    {
        BoardManager.Instance.boardStatus[pieceCoord.col][pieceCoord.row] = null;
        this.gameObject.SetActive(false);
    }

    public void ResetBoardCoord()
    {
        pieceCoord = new BoardCoord(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        this.boardManager = BoardManager.Instance;

        pieceCoord = new BoardCoord(transform.position.x, transform.position.z);
    }

}