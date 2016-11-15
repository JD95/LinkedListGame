using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level6Logic : WinCondition {

    public GameNode[] firstgroup = new GameNode[4];

	public GameNode[] groupOfNodes = new GameNode[5];

    // Use this for initialization
    void Start () {
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {

                for (int i = 0; i < 4; i++)
				    firstgroup[i] = board.addNewNodeReturn (true, false);

                GameBoard.nodes.first = firstgroup[0];
                for (int i = 0; i < 3; i++)
                {
                    firstgroup[i].next = firstgroup[i+1];
                }              
                GameBoard.nodes.last = firstgroup[3];

                board.currentPointer.pointTo(firstgroup[0]);

                board.nextPointer.pointTo(firstgroup[1]);
                board.nextPointer.togglePointer();

                board.nextNextPointer.pointTo(firstgroup[2]);
                board.nextNextPointer.togglePointer();

                return "Move the current pointer to " + firstgroup[3].nodeValue;

            }, () => board.currentPointer.node == firstgroup[3]),

			// Win state
			new Stage(() => "Winner!", () => false)
		};

		progress ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
