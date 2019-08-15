using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Adapter", menuName = "Skill Adapter", order = 0)]
class SkillAdapterDistanceOption : Skill
{
    public override void Operate(BoardCoord selectedBoardCoord)
    {
        var target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();
        EffectManager.Instance.AddEffect(target, this);
    }
}