using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Board
{
    public GameObject[,] spaces = new GameObject[10, 10];
	public GameBoard game;

    public Board()
    {

    }

	public Board(GameBoard game){
		this.game = game;
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

	bool leak(GameObject g, GameLinkedList nodes, List<GameNode> newElements)
    {
         return !(g == null 
             || nodes.find(g, newElements) != null
             || game.currentPointer.node != null && game.currentPointer.node.value == g
             || game.nextPointer.node != null && game.nextPointer.node.value == g
             || game.nextNextPointer.node != null && game.nextNextPointer.node.value == g);
             
    }

	public bool noLeaks(GameLinkedList nodes, List<GameNode> newElements)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (leak(spaces[i, j], nodes, newElements)) return false;
            }
        }

        return true;
    }
}

