using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level3Logic : WinCondition {

    public GameNode[] firstgroup = new GameNode[2];

	public GameNode[] groupOfNodes = new GameNode[5];

    // Use this for initialization
    void Start () {
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {

				firstgroup[0] = board.addNewNodeReturn (true, false);
                firstgroup[1] = board.addNewNodeReturn (true, false);
					
                GameBoard.nodes.first = firstgroup[0];
                firstgroup[0].next = firstgroup[1];
                GameBoard.nodes.last = firstgroup[1];

                board.currentPointer.pointTo(firstgroup[0]);

                return "Move the current pointer";

            }, () => board.currentPointer.node == firstgroup[1]),

            // Stage 2
            new Stage(() => {

                foreach (var node in firstgroup) {
                    GameBoard.masterList.Remove(node);
                    node.deleteNode(false);
                }

                GameBoard.log.wipeAll();

                for (int i = 0; i < 5; i++)
                    groupOfNodes[i] = board.addNewNodeReturn(true, false);

                for (int i = 0; i < 4; i++)
                {
                    groupOfNodes[i].next = groupOfNodes[i + 1];
			    }

                board.currentPointer.pointTo(groupOfNodes[0]);

                return "Move the current pointer to " + groupOfNodes[4].nodeValue;

            }, () => board.currentPointer.node == groupOfNodes[4]),

            // Stage 3
            new Stage(() => {

                foreach (var node in groupOfNodes)
                    node.deleteNode(false);

                GameBoard.log.wipeAll();

                for (int i = 0; i < 5; i++)
                    groupOfNodes[i] = board.addNewNodeReturn(true, false);

                board.currentPointer.pointTo(groupOfNodes[0]);
                GameBoard.nodes.first = groupOfNodes[0];
                GameBoard.nodes.last = groupOfNodes[0];

               return "Please connect nodes in this order: " + groupOfNodes.Select(n => n.nodeValue + " ").Aggregate("",(l,r) => l + r);

            }, () => GameBoard.nodes.listIs(groupOfNodes.Select(n => n.nodeValue).ToList())),

            // Stage 4
            new Stage(() => {
                return "Move current pointer to " + groupOfNodes[4].nodeValue;
            }, () => board.currentPointer.node == groupOfNodes[4]),


			// Win state
			new Stage(() => "Winner!", () => false)
		};

		progress ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
