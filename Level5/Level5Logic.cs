using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class Level5Logic : WinCondition
{

    public GameNode[] firstgroup = new GameNode[3];

    public GameNode[] groupOfNodes = new GameNode[5];

    // Use this for initialization
    void Start()
    {

        stages = new List<Stage>(){
			// Stage 1
			new Stage(() => {

                firstgroup[0] = board.addNewNodeReturn (true, false);
                firstgroup[1] = board.addNewNodeReturn (true, false);
                firstgroup[2] = board.addNewNodeReturn (true, false);

                GameBoard.nodes.first = firstgroup[0];
                GameBoard.nodes.last = firstgroup[0];

                /*
                firstgroup[0].next = firstgroup[1];
                firstgroup[0].next.next = firstgroup[2];
                GameBoard.nodes.last = firstgroup[2];
                */

                board.currentPointer.pointTo(firstgroup[0]);
                //board.nextPointer.toggleNode();
                //board.nextNextPointer.toggleNode();

                return "Connect " + firstgroup[0].nodeValue + "->" + firstgroup[1].nodeValue + " and " + firstgroup[1].nodeValue + "->" + firstgroup[2].nodeValue;

            }, () => GameBoard.nodes.listIs(firstgroup.Select(n => n.nodeValue).ToList())),

            // Stage 2
            new Stage(() => {

                return "Toggle Next Pointer";

            }, () => board.nextPointer.isActive),

            //Stage 3
            new Stage(() => {

                return "Toggle Next Next Pointer";

            }, () => board.nextPointer.isActive && board.nextNextPointer.isActive),

            //Stage 4
            new Stage(() => {

                foreach (var node in firstgroup) {
                    node.deleteNode(false);
                    GameBoard.masterList.Remove(node);
                }

                GameBoard.log.wipeAll();

                for (int i = 0; i < 5; i++)
                    groupOfNodes[i] = board.addNewNodeReturn(true, false);

                for (int i = 0; i < 4; i++)
                {
                    groupOfNodes[i].next = groupOfNodes[i + 1];
                }

				GameBoard.nodes.first = groupOfNodes[0];

                board.currentPointer.pointTo(groupOfNodes[0]);

				board.nextPointer.pointTo(groupOfNodes[1]);
				//board.nextPointer.togglePointer();

				board.nextNextPointer.pointTo(groupOfNodes[2]);
				//board.nextNextPointer.togglePointer();

                return "Toggle Next Next Pointer on " + groupOfNodes[4].nodeValue;

            }, () => board.nextNextPointer.node == groupOfNodes[4]),

			// Win state
			new Stage(() => "Winner!", () => false)
        };

        progress();
    }
}
