using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Board
{
    public GameObject[,] spaces = new GameObject[10, 10];

    public Board()
    {

    }

    public bool emptyBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (spaces[i, j] != null) return false;
            }
        }

        return true;
    }

    bool leak(GameObject g, GameLinkedList nodes)
    {
        return !(g == null || nodes.find(g, new List<GameNode>()));
    }

    public bool noLeaks(GameLinkedList nodes)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (leak(spaces[i, j], nodes)) return false;
            }
        }

        return true;
    }
}

