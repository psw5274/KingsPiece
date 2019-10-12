using PieceSystem;
using UnityEngine;

public class KingIsMatter : MonoBehaviour
{
    public void OnDisable()
    {
        var piece = GetComponent<Piece>();

        Debug.Log($"{piece.GetTeamColor()} is loose");
    }
}