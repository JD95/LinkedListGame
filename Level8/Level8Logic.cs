using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level8Logic : WinCondition {

    public GameNode[] firstgroup = new GameNode[3];

	public GameNode[] groupOfNodes = new GameNode[5];

    // Use this for initialization
    void Start () {
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {

                for (int i = 0; i < 3; i++)
				    firstgroup[i] = board.addNewNodeReturn (true, false);

                GameBoard.nodes.first = firstgroup[0];
                for (int i = 0; i < 2; i++)
                    firstgroup[i].next = firstgroup[i+1];
                GameBoard.nodes.last = firstgroup[2];

                board.currentPointer.pointTo(firstgroup[0]);

                board.nextPointer.pointTo(firstgroup[1]);
                board.nextPointer.togglePointer();

                board.nextNextPointer.pointTo(firstgroup[2]);
                board.nextNextPointer.togglePointer();

                return "Delete next pointer";

            }, () => board.nextPointer.node.deleted),

            // Stage 2
			new Stage(() => {

                return "Point current at the next next pointer";

            }, () => GameBoard.nodes.listIs(new List<string> {
                firstgroup[0].nodeValue,
                firstgroup[2].nodeValue,
            })),

			// Win state
			new Stage(() => "Winner!", () => false)
		};

		progress ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
