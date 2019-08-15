using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Adapter", menuName = "Skill Adapter", order = 0)]
class SkillAdapterDistanceOption : Skill
{
    [Serializable]
    private class DistanceOption : IComparable<DistanceOption>
    {
        public enum CompareType
        {
            Equal,
            Greater,
            Lesser,
            GreaterAndEqual,
            LesserAndEqual
        }
        public int distance;
        public CompareType comparer;
        public Skill skill;

        public int CompareTo(DistanceOption other)
        {
            return distance.CompareTo(other.distance);
        }
    }

    [SerializeField]
    private DistanceOption[] options;

    public override void Operate(BoardCoord selectedBoardCoord)
    {
        Piece piece = BoardManager.Instance.selectedPiece.GetComponent<Piece>();
        Piece target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();

        int distance = BoardCoord.Distance(piece.pieceCoord, target.pieceCoord);

        var selectedOptions = options.Where(distanceOption => DistanceCompare(distanceOption, distance));

        if (selectedOptions.Count() == 0)
        {
            EffectManager.Instance.AddEffect(target, this);
            return;
        }

        var option = selectedOptions.Max();
        EffectManager.Instance.AddEffect(target, option.skill);
    }

    private bool DistanceCompare(DistanceOption option, int distance)
    {
        switch (option.comparer)
        {
            case DistanceOption.CompareType.Equal:
                return option.distance == distance;
            case DistanceOption.CompareType.Greater:
                return option.distance > distance;
            case DistanceOption.CompareType.GreaterAndEqual:
                return option.distance >= distance;
            case DistanceOption.CompareType.Lesser:
                return option.distance < distance;
            case DistanceOption.CompareType.LesserAndEqual:
                return option.distance <= distance;
            default:
                return false;
        }
    }
}
