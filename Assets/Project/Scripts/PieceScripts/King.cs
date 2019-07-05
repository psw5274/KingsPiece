using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override void Initialize(TeamColor teamColor, HeroCard heroCard)
    {
        base.Initialize(teamColor, heroCard);
        movingDirection = MovingDirection.Both;
        movingDistance = 1;
    }

    public override bool UseSkill()
    {

        return true;
    }

    protected override void Die()
    {
        base.Die();
        GameManager.Instance.SetWinner(teamColor == TeamColor.White ? TeamColor.Black : TeamColor.White);
    }
}
