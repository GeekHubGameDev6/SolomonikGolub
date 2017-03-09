using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic
{
    private int size;
    private int[,] map;
    int spaceX, spaceY;

    public PuzzleLogic(int size)
    {// Max and min puzzle matrix size
        if (size < 2)
            size = 2;
        else if (size >= 9)
            size = 9;

        this.size = size;
        map = new int[size, size];
    }

    public void Start()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                map[x, y] = CoordsToPosition(x, y);
            }
        }

        spaceX = size - 1;
        spaceY = size - 1;
        map[spaceX, spaceY] = -1;
    }

    public void Shift(int position)
    {
        int x, y;
        PositionToCoords(position, out x, out y);

        map[spaceX, spaceY] = map[x, y];
        map[x, y] = -1;
        spaceX = x;
        spaceY = y;
    }

    public bool CheckNumbers()
    {
        if (!(spaceX == size - 1 && spaceY == size - 1))
            return false;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (!(x == size - 1 && y == size - 1))
                    if (map[x, y] != CoordsToPosition(x, y))
                        return false;
            }
        }

        return true;
    }

    public int GetNumber(int position)
    {
        int x, y;
        PositionToCoords(position, out x, out y);
        if (x < 0 || x >= size)
            return 0;
        if (y < 0 || y >= size)
            return 0;

        return map[x, y];
    }

    internal void ShiftRandom()
    {
        int x = spaceX;
        int y = spaceY;


        // One random step Up|Down|Left|Right
        int a = UnityEngine.Random.Range(0, 4);
        switch (a)
        {
            case 0:
                x--;
                break;
            case 1:
                x++;
                break;
            case 2:
                y--;
                break;
            case 3:
                y++;
                break;
        }

        Shift(CoordsToPosition(x, y));
    }

    private void PositionToCoords(int position, out int x, out int y)
    {
        if (position < 0)
            position = 0;
        if (position > size * size - 1)
            position = size * size - 1;

        x = position % size;
        y = position / size;
    }

    private int CoordsToPosition(int x, int y)
    {
        if (x < 0)
            x = 0;
        if (x > size - 1)
            x = size - 1;

        if (y < 0)
            y = 0;
        if (y > size - 1)
            y = size - 1;

        return y * size + x;
    }
}
