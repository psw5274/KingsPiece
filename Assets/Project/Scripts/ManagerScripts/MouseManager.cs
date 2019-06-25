using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class MouseManager : MonoBehaviour
{
    private float mousePositionX { get; set; }
    private float mousePositionY { get; set; }

    private void GetMousePosition()
    {
        if (!Camera.main) return;

        RaycastHit hit;
        float raycastDistance = 1000.0f;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                            out hit, raycastDistance, LayerMask.GetMask("ChessBoard"))) //, LayerMask.GetMask("ChessPlane")))
        {
            mousePositionX = hit.point.x;
            mousePositionY = hit.point.z;
        }
        else
        {
            mousePositionX = int.MaxValue;
            mousePositionY = int.MaxValue;
        }
    }
    
    void ClickBoard(int boardPositionX, int boardPositionY)
    {
        if (BoardManager.Instance.isPieceSelected)
            Debug.Log(BoardManager.Instance.SetPieceDestination(boardPositionX, boardPositionY));
        else
            BoardManager.Instance.SelectPiece(boardPositionX, boardPositionY);
    }

	void Awake ()
    {
    }
	

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetMousePosition();

            int boardPositionX = BoardManager.Instance.GetBoardCoord(mousePositionX);
            int boardPositionY = BoardManager.Instance.GetBoardCoord(mousePositionY);
            
            if (boardPositionX < 8 && boardPositionY < 8)
            {
                ClickBoard(boardPositionX, boardPositionY);
            }
        }
    }
}
*/