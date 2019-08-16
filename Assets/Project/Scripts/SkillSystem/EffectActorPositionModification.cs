public class EffectActorPositionModification : Effect
{
    public EffectActorPositionModification(Piece target, Skill.ApplyingTiming timing, int remainDuration)
        : base(target, timing, remainDuration)
    {
    }
    
    public override void Active()
    { 
        Piece actor = BoardManager.Instance.selectedPiece.GetComponent<Piece>();
        BoardCoord targetCoord = BoardManager.Instance.GetClickedCoord();
        int distance = BoardCoord.Distance(actor.pieceCoord, targetCoord);
        BoardCoord direction = (targetCoord - actor.pieceCoord).GetDirectionalCoord();

        actor.Move(actor.pieceCoord + (direction * (distance - 1)));
    }

    public override void Deactive()
    {
    }

}