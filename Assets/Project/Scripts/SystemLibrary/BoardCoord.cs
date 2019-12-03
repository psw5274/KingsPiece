using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 보드 위의 좌표에 대한 구조체. 유니티 좌표계의 Z축이 boardY에 해당된다.
/// </summary>
[System.Serializable]
public struct BoardCoord
{
    public static readonly BoardCoord RIGHT = new BoardCoord(1, 0);
    public static readonly BoardCoord UP = new BoardCoord(0, 1);
    public static readonly BoardCoord LEFT = new BoardCoord(-1, 0);
    public static readonly BoardCoord DOWN = new BoardCoord(0, -1);

    public static readonly BoardCoord UP_RIGHT = new BoardCoord(1, 1);
    public static readonly BoardCoord DOWN_RIGHT = new BoardCoord(1, -1);
    public static readonly BoardCoord UP_LEFT = new BoardCoord(-1, 1);
    public static readonly BoardCoord DOWN_LEFT = new BoardCoord(-1, -1);

    private const float BOARD_CELL_LENGTH = 9.0f;

    public int col; // x
    public int row; // y

    public BoardCoord(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public BoardCoord(float x, float z)
    {

        col = (int)((x + (BOARD_CELL_LENGTH * BoardManager.NUM_BOARD_ROW / 2)) / BOARD_CELL_LENGTH);
        row = (int)((z + (BOARD_CELL_LENGTH * BoardManager.NUM_BOARD_COL / 2)) / BOARD_CELL_LENGTH);
    }

    public bool IsAvailable()
    {
        return !(col < 0 || row < 0 ||
               BoardManager.NUM_BOARD_COL <= col ||
               BoardManager.NUM_BOARD_ROW <= row);
    }

    public Vector3 GetBoardCoardVector3()
    {
        return new Vector3((col - 3.5f) * BOARD_CELL_LENGTH,
                            7.21f,
                            (row - 3.5f)* BOARD_CELL_LENGTH);
    }

    public static Vector3 GetBoardCoordVector3(int x, int z)
    {
        return new Vector3((z - 3.5f) * BOARD_CELL_LENGTH,
                            7.21f,
                            (x - 3.5f) * BOARD_CELL_LENGTH);
    }

    public BoardCoord GetDirectionalCoord()
    {
        return new BoardCoord(col == 0 ? col : col / Mathf.Abs(col),
                              row == 0 ? row : row / Mathf.Abs(row));
    }

    public void ReverseBoardCoord()
    {
        this.col = BoardManager.NUM_BOARD_COL - col - 1;
        this.row = BoardManager.NUM_BOARD_ROW - row - 1;
    }

    public static BoardCoord GetReverseBoardCoord(int col, int row)
    {
        col = BoardManager.NUM_BOARD_COL - col - 1;
        row = BoardManager.NUM_BOARD_ROW - row - 1;

        return new BoardCoord(col, row);
    }


    /// <summary>
    /// 보드 좌표에 대한 연산자 오버로딩
    /// </summary>
    /// <returns>새로운 보드 좌표값</returns>
    public static BoardCoord operator +(BoardCoord coord1, BoardCoord coord2)
    {
        return new BoardCoord(coord1.col + coord2.col, coord1.row + coord2.row);
    }
    public static BoardCoord operator -(BoardCoord coord1, BoardCoord coord2)
    {
        return new BoardCoord(coord1.col - coord2.col, coord1.row - coord2.row);
    }
    public static BoardCoord operator *(BoardCoord coord, int n)
    {
        return new BoardCoord(coord.col * n, coord.row * n);
    }
    public static bool operator ==(BoardCoord b1, BoardCoord b2)
    {
        if (object.ReferenceEquals(b1, null) || object.ReferenceEquals(b2, null))
        {
            return false;
        }
        return b1.col == b2.col && b1.row == b2.row ? true : false;
    }
    public static bool operator !=(BoardCoord b1, BoardCoord b2)
    {
        return !(b1 == b2);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return base.ToString();
    }
}
