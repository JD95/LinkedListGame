using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;


public class Level1Logic : WinCondition {

    public GameNode firstNode;

	public GameNode[] groupOfNodes = new GameNode[3];

    // Use this for initialization
    void Start () {
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {
				firstNode = board.addNewNodeReturn (true, false);

				for (int i = 0; i < 3; i++)
					groupOfNodes [i] = board.addNewNodeReturn (false, false);

				firstNode.value.SetActive(true);

				foreach(var node in groupOfNodes)
					node.value.SetActive(false);

                return "Please delete the node";

            }, () => !firstNode.isActiveAndEnabled),

			// Stage 2
			new Stage(() => { 
				firstNode.deleteNode(false);
				foreach (var node in groupOfNodes)
					node.value.SetActive(true);

                return "Please delete all of the nodes";

            }, () => groupOfNodes.Select(n => !n.value.activeSelf).Aggregate(true, (l,r) => l && r)),

			// Win state
			new Stage(() => "Winner!", () => false)
		};

		progress ();
	}
}
