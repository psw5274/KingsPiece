using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill", order = 0)]
public class Skill : ScriptableObject
{
    public enum ApplyingTiming { Instant, Attack, Damaged, Moved, TurnPassed }
    [Serializable]
    public class Parameter
    {
        public enum ModifyTarget
        {
            HPAddition,
            HPMultiplication,
            ATKAddition,
            ATKMultiplication,
            Movability,
            Unbeatability,
            HPHeal,
            HPDamage
        }

        public ModifyTarget target;
        public int value;
    }

    public string label;
    public int duration;
    public ApplyingTiming timing;
    public Parameter[] parameters;

    public List<BoardCoord> GetAvailableTargetCoord()
    {
        List<BoardCoord> targetCoordList = new List<BoardCoord>();

        foreach (GameObject[] rows in BoardManager.Instance.boardStatus)
        {
            foreach (GameObject pieceObject in rows)
            {
                if (pieceObject == null)
                {
                    continue;
                }

                targetCoordList.Add(pieceObject.GetComponent<Piece>().pieceCoord);
            }
        }

        return targetCoordList;
    }

    public void Operate(BoardCoord selectedBoardCoord)
    {
        var target = BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row].GetComponent<Piece>();
        EffectManager.Instance.AddEffect(target, this);

    }
}
