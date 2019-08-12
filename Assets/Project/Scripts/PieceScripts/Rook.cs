using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{

    public override void Initialize(TeamColor teamColor, HeroCard heroCard)
    {
        base.Initialize(teamColor,heroCard);
        movingDirection = MovingDirection.Straight;
    }
}
