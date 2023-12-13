using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBuilder : MonoBehaviour
{
    [SerializeField] private List<Transform> blocks;

    public Piece buildPiece()
    {
        return new Piece(calcOffsets());
    }

    private Vector2[] calcOffsets()
    {
        Transform origin = blocks[0];
        Vector2[] offsets = new Vector2[blocks.Count];
        for (int i = 0; i < blocks.Count; i++)
        {
            Vector2 offset = origin.position - blocks[i].position;
            offsets[i] = new Vector2(-offset.x, offset.y);
        }
        return offsets;
    }
}
