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
            [Range(1, 8)]
            private int repeat = 1;
            [SerializeField]
            private BoardCoord[] deltas = null;

            public BoardCoord Query(BoardCoord center, TeamColor targetColor)
            {
                foreach (BoardCoord delta in deltas)
                {
                    for (int repeated = 0; repeated < repeat; ++repeated)
                    {
                        BoardCoord targetCoordination = center + delta + (delta * repeated);
                        if (targetCoordination.IsAvailable() == false)
                        {
                            break;
                        }
                        GameObject gameobject = BoardManager.Instance.boardStatus[targetCoordination.col][targetCoordination.row];
                        if (gameobject == null)
                        {
                            continue;
                        }
                        Piece piece = gameobject.GetComponent<Piece>();
                        if (targetColor != TeamColor.Both && piece.teamColor != targetColor)
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
        private Sequentional[] sequentionals;

        public HashSet<BoardCoord> Query(BoardCoord center, TeamColor targetColor)
        {
            HashSet<BoardCoord> targetCoordinations = new HashSet<BoardCoord>();

            foreach (Sequentional sequence in sequentionals)
            {
                targetCoordinations.Add(sequence.Query(center, targetColor));
            }

            targetCoordinations.Remove(BoardCoord.NEGATIVE);

#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Grid Query is finish (Result can see below)\n";
            DEBUG_STRING += $"using center(({center.col}, {center.row})), target color({targetColor.ToString()})\n";
            DEBUG_STRING += $"***\t\t\t***\n";
            foreach (var coordination in targetCoordinations)
            {
                DEBUG_STRING += $"({coordination.col}, {coordination.row})\n";
            }
            DEBUG_STRING += $"***\t\t\t***\n";
            Debug.Log(DEBUG_STRING);
        }
#endif
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
                if (targetColor != TeamColor.Both && piece.teamColor != targetColor)
                {
                    continue;
                }

                targetCoordinations.Add(piece.pieceCoord);
            }
        }

#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Grid Query FOR ALL is finish (Result can see below)\n";
            DEBUG_STRING += $"using target color({targetColor.ToString()})\n";
            DEBUG_STRING += $"***\t\t\t***\n";
            foreach (var coordination in targetCoordinations)
            {
                DEBUG_STRING += $"({coordination.col}, {coordination.row})\n";
            }
            DEBUG_STRING += $"***\t\t\t***\n";
            Debug.Log(DEBUG_STRING);
        }
#endif
        return targetCoordinations;
    }
}