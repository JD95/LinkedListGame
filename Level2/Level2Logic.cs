using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;



public class Level2Logic : WinCondition {

    public GameNode[] firstgroup = new GameNode[2];

	public GameNode[] groupOfNodes = new GameNode[3];

    // Use this for initialization
    void Start () {
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {

				firstgroup[0] = board.addNewNodeReturn (true, false);
                firstgroup[1] = board.addNewNodeReturn (true, false);
					
                GameBoard.nodes.first = firstgroup[0];

                return "Please connect " + firstgroup[0].nodeValue + " to " + firstgroup[1].nodeValue;

            }, () => GameBoard.nodes.length() == 2),

			// Stage 2
			new Stage(() => {

                foreach(var node in firstgroup)
                    node.deleteNode(false);

                for (int i = 0; i < 3; i++)
                    groupOfNodes [i] = board.addNewNodeReturn (true, false);


                GameBoard.nodes.first = groupOfNodes[0];
                GameBoard.nodes.last = groupOfNodes[0];

                return "Please connect nodes in this order: " + groupOfNodes.Select(n => n.nodeValue + " ").Aggregate("",(l,r) => l + r);

            }, () => GameBoard.nodes.listIs(groupOfNodes.Select(n => n.nodeValue).ToList())),

			// Win state
			new Stage(() => "Winner!", () => false)
		};

		progress ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
