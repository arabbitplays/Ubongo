using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private static Vector2[] HOLE_CHECK_OFFSETS =
    {
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, 1),
        new Vector2(0, -1),

    };

    private List<Piece> pieces;
    private int[,] slots;

    public Board(int[,] slots)
    {
        this.slots = slots;
        initSlotValues();
        this.pieces = new List<Piece>();
    }

    private void initSlotValues()
    {
        for (int y = 0; y < slots.GetLength(0); y++)
        {
            for (int x = 0; x < slots.GetLength(1); x++)
            {
                Vector2 pos = new Vector2(x, y);
                if (getSlot(pos) == -1)
                    continue;
                int slotValue = 0;
                foreach (Vector2 checkOffset in HOLE_CHECK_OFFSETS)
                {
                    if (getSlot(pos + checkOffset) == -1)
                    {
                        slotValue++;
                    }
                }
                setSlot(pos, slotValue);
            }
        }
    }

    public bool pieceFits(Piece piece, Vector2 pos)
    {
        foreach (Vector2 offset in piece.getOffsets())
        {
            Vector2 slot = pos + offset;
            if (getSlot(slot) == -1)
                return false;
        }
        return true;
    }

    public bool setPiece(Piece piece)
    {
        bool createsHoles = false;
        Vector2[] offsets = piece.getOffsets();

        List<int> prevSlotValues = new List<int>();
        foreach(Vector2 offset in offsets)
        {
            Vector2 slot = piece.getPos() + offset;
            prevSlotValues.Add(getSlot(slot));
            setSlot(slot, -1);
        }

        // check for holes
        foreach (Vector2 offset in offsets)
        {
            Vector2 slot = piece.getPos() + offset;
            foreach (Vector2 checkOffset in HOLE_CHECK_OFFSETS)
            {
                Vector2 slotToCheck = slot + checkOffset;
                int slotValue = getSlot(slotToCheck);
                if (slotValue == -1)
                    continue;
                if (slotValue == 3)
                {
                    createsHoles = true;
                }
                setSlot(slotToCheck, slotValue + 1);
            }
        }

        if (createsHoles)
        {
            // reduce values back
            foreach (Vector2 offset in offsets)
            {
                Vector2 slot = piece.getPos() + offset;
                foreach (Vector2 checkOffset in HOLE_CHECK_OFFSETS)
                {
                    Vector2 slotToCheck = slot + checkOffset;
                    int slotValue = getSlot(slotToCheck);
                    if (slotValue == -1)
                        continue;
                    setSlot(slotToCheck, slotValue - 1);
                }
            }

            // restored set slots
            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2 slot = piece.getPos() + offsets[i];
                setSlot(slot, prevSlotValues[i]);
            }

            return false;
        } else
        {
            pieces.Add(piece);
            return true;
        }
    }

    public void unsetPiece(Piece piece)
    {
        foreach (Vector2 offset in piece.getOffsets())
        {
            Vector2 slot = piece.getPos() + offset;
            setSlot(slot, 0);
        }
        pieces.Remove(piece);
    }

    public void printBoard()
    {
        string str = "\n";
        for (int y = 0; y < slots.GetLength(0); y++)
        {
            for (int x = 0; x < slots.GetLength(1); x++)
            {
                str += " " + getSlot(new Vector2(x, y));
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    public int getSlot(Vector2 pos)
    {
        if (pos.x < 0 || pos.x >= getSize().x || pos.y < 0 || pos.y >= getSize().y)
            return -1;
        return slots[(int)pos.y, (int)pos.x];
    }

    private void setSlot(Vector2 pos, int value)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if (x < 0 || x >= getSize().x || y < 0 || y >= getSize().y)
            return;
        slots[y, x] = value;
    }

    public bool isSolved()
    {
        for (int y = 0; y < slots.GetLength(0); y++)
        {
            for (int x = 0; x < slots.GetLength(1); x++)
            {
                if (!(getSlot(new Vector2(x, y)) == -1))
                    return false;

            }
        }
        return true;
    }

    public Vector2 getSize()
    {
        return new Vector2(slots.GetLength(1), slots.GetLength(0));
    }

    public List<Piece> getPieces()
    {
        return pieces;
    }
}
