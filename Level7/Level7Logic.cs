using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level7Logic : WinCondition {

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
                firstgroup[0].next = firstgroup[1];          
                GameBoard.nodes.last = firstgroup[1];

                board.currentPointer.pointTo(firstgroup[0]);

                board.nextPointer.pointTo(firstgroup[1]);
                board.nextPointer.togglePointer();

                return "Point current pointer to node " + firstgroup[2].nodeValue;

            }, () => GameBoard.nodes.listIs(new List<string> {
                firstgroup[0].nodeValue,
                firstgroup[2].nodeValue,
            })),

            // Stage 2
			new Stage(() => {

                return "Point node " + firstgroup[2].nodeValue + " at next pointer";

            }, () => GameBoard.nodes.listIs(new List<string> {
                firstgroup[0].nodeValue,
                firstgroup[2].nodeValue,
                firstgroup[1].nodeValue
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
