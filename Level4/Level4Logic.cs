using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

/* Connect Next Ptr to new elements
 * Stg 1:
 *  [0]-> 10 -> 3
 *  Condition: List [0, 10, 3]
 *  
 * Stg 2:
 *  [13] -> 21 -> 7 -> 63
 *  Condition: List [13, 21, 7, 63]
 */
public class Level4Logic : WinCondition
{

    public GameNode[] firstgroup = new GameNode[3];

    public GameNode[] groupOfNodes = new GameNode[4];

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
                firstgroup[0].next = firstgroup[1];
                GameBoard.nodes.last = firstgroup[1];

                board.currentPointer.pointTo(firstgroup[0]);
                board.nextPointer.toggleNode();

                return "Toggle Next Pointer";

            }, () => board.nextPointer.isActive),

            // Stage 2
            new Stage(() => {

                return "Point Next Pointer to " + firstgroup[2].nodeValue;

            }, () => GameBoard.nodes.listIs(firstgroup.Select(n => n.nodeValue).ToList())),

            //Stage 3
            new Stage(() => {

                foreach (var node in firstgroup)
                    node.deleteNode(false);

                for (int i = 0; i < 4; i++)
                    groupOfNodes[i] = board.addNewNodeReturn(true, false);

                for (int i = 0; i < 3; i++)
                {
                    groupOfNodes[i].next = groupOfNodes[i + 1];
                }

                board.currentPointer.pointTo(groupOfNodes[0]);

                return "Toggle Next Pointer on " + groupOfNodes[3].nodeValue;

            }, () => board.nextPointer.node == groupOfNodes[3]),

			// Win state
			new Stage(() => "Winner!", () => false)
        };

        progress();
    }
}
