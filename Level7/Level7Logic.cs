using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level7Logic : WinCondition {

    public GameNode[] firstgroup = new GameNode[3];

	public GameNode[] groupOfNodes = new GameNode[6];

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

                return "Point current pointer to node " + firstgroup[2].nodeValue;

            }, () => GameBoard.nodes.listIs(new List<string> {
                firstgroup[0].nodeValue,
                firstgroup[2].nodeValue,
            })),

            // Stage 2
			new Stage(() => {

                return "Point node " + firstgroup[2].nodeValue + " at the next pointer";

            }, () => GameBoard.nodes.listIs(new List<string> {
                firstgroup[0].nodeValue,
                firstgroup[2].nodeValue,
                firstgroup[1].nodeValue
            })),

            // Stage 3
			new Stage(() => {

                foreach (var node in firstgroup)
                {
                    GameBoard.masterList.Remove(node);
                    node.deleteNode(false);
                }

                for (int i = 0; i < 5; i++)
                    groupOfNodes[i] = board.addNewNodeReturn(false, false);

                for (int i = 0; i < 4; i++)
                    groupOfNodes[i].next = groupOfNodes[i + 1];

                groupOfNodes[5] = board.addNewListNodeReturn(true, false);

                GameBoard.nodes.first = groupOfNodes[0];
                       
                board.currentPointer.pointTo(groupOfNodes[0]);

                board.nextPointer.pointTo(groupOfNodes[1]);
                board.nextPointer.togglePointer();

                board.nextNextPointer.pointTo(groupOfNodes[2]);
                board.nextNextPointer.togglePointer();

                return "Insert node " + groupOfNodes[5].nodeValue + " after node " + groupOfNodes[3].nodeValue;

            }, () => GameBoard.nodes.listIs(new List<string> {
                groupOfNodes[0].nodeValue,
                groupOfNodes[1].nodeValue,
                groupOfNodes[2].nodeValue,
                groupOfNodes[3].nodeValue,
                groupOfNodes[5].nodeValue,
                groupOfNodes[4].nodeValue
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
