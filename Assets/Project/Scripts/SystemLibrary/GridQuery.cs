using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridQuery
{
    [Serializable]
    private class Combinational
    {
        [Serializable]
        private class Sequentional
        {
            [SerializeField]
            private bool repeat = false;
            [SerializeField]
            private BoardCoord[] deltas = null;

            public BoardCoord Query(BoardCoord center, TeamColor targetColor)
            {
                if (repeat)
                {
                    return QueryRepeat(center, targetColor);
                }
                else
                {
                    return QueryOnce(center, targetColor);
                }
            }

            private BoardCoord QueryOnce(BoardCoord center, TeamColor targetColor)
            {
                foreach (BoardCoord delta in deltas)
                {
                    BoardCoord targetCoordination = center + delta;
                    GameObject gameobject = BoardManager.Instance.boardStatus[targetCoordination.col][targetCoordination.row];
                    if (gameobject == null)
                    {
                        continue;
                    }
                    
                    return targetCoordination;
                }

                return BoardCoord.NEGATIVE;
            }

            private BoardCoord QueryRepeat(BoardCoord center, TeamColor targetColor)
            {
                foreach (BoardCoord delta in deltas)
                {
                    while (true)
                    {
                        BoardCoord targetCoordination = center + delta;
                        if (targetCoordination.IsAvailable() == false)
                        {
                            break;
                        }

                        GameObject gameobject = BoardManager.Instance.boardStatus[targetCoordination.col][targetCoordination.row];
                        if (gameobject == null)
                        {
                            continue;
                        }

                        return targetCoordination;
                    }
                }

                return BoardCoord.NEGATIVE;
            }
        }

        [SerializeField]
        private Sequentional[] sequentionals = null;

        public HashSet<BoardCoord> Query(BoardCoord center, TeamColor targetColor)
        {
            HashSet<BoardCoord> targetCoordinations = new HashSet<BoardCoord>();

            foreach (Sequentional sequence in sequentionals)
            {
                targetCoordinations.Add(sequence.Query(center, targetColor));
            }

            targetCoordinations.Remove(BoardCoord.NEGATIVE);
            return targetCoordinations;
        }
    }

    [SerializeField]
    private bool isTargetingAll = false;
    [SerializeField]
    private Combinational combinational = null;

    public HashSet<BoardCoord> TargetCoordinationQuery(BoardCoord center, TeamColor targetColor)
    {
        return isTargetingAll ? AllQuery(targetColor) : combinational.Query(center, targetColor);
    }

    private HashSet<BoardCoord> AllQuery(TeamColor targetColor)
    {
        HashSet<BoardCoord> targetCoordinations = new HashSet<BoardCoord>();

        foreach (GameObject[] rows in BoardManager.Instance.boardStatus)
        {
            foreach (GameObject pieceObject in rows)
            {
                if (pieceObject == null)
                {
                    continue;
                }
                Piece piece = pieceObject.GetComponent<Piece>();
                if (piece.teamColor != targetColor)
                {
                    continue;
                }

                targetCoordinations.Add(piece.pieceCoord);
            }
        }

        return targetCoordinations;
    }
}