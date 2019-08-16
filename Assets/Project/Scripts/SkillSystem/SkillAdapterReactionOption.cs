using UnityEngine;

[CreateAssetMenu(fileName = "New Skill With Reaction Option", menuName = "Skill Adapter/Reaction Option", order = 0)]
public class SkillAdapterReactionOption : Skill
{
    [SerializeField]
    private Skill skillForReaction;

    public override void Operate(BoardCoord selectedBoardCoord)
    {
        Piece target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();
        base.Operate(selectedBoardCoord);
        skillForReaction.Operate(BoardManager.Instance.selectedPiece.GetComponent<Piece>().pieceCoord);
    }
}