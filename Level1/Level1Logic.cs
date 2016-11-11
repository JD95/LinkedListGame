using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class Stage {

	public Func<bool> progressCheck;
	public Action progress;
	public string instruction;

	public Stage (string i, Func<bool> p, Action pg){
		this.progressCheck = p;
		this.progress = pg;
		this.instruction = i;
	}
}

public class Level1Logic : WinCondition {

	public Text instructions;

    public GameBoard board;

    public GameNode firstNode;

	public GameNode[] groupOfNodes = new GameNode[3];

	int stage = 0;
	List<Stage> stages;

	private void setInstructionText(string text){
		instructions.text = text;
	}

    public override bool canProgress()
    {
		return (stage >= stages.Count) ? false : stages[stage].progressCheck();
    }

    public override void progress()
    {
		stages [stage++].progress ();
		setInstructionText(stages [stage].instruction);
    }

    public override bool win()
    {
        foreach (var node in groupOfNodes)
        {
            if (node != null) return false;
        }

        return true;
    }

    // Use this for initialization
    void Start () {

		firstNode = board.addNewNodeReturn ();

        for (int i = 0; i < 3; i++)
        {
			groupOfNodes [i] = board.addNewNodeReturn ();
        }

		firstNode.value.SetActive(true);
        foreach(var node in groupOfNodes)
        {
			node.value.SetActive(false);
        }

		stages = new List<Stage>(){
			// Stage 1
			new Stage( "Please delete the node",
				() => !firstNode.isActiveAndEnabled,
				() => { 
					firstNode.deleteNode();
					foreach (var node in groupOfNodes)
						node.value.SetActive(true);
				}),
			// Stage 2
			new Stage("Please delete all of the nodes",
				() => groupOfNodes.Select(n => !n.value.activeSelf).Aggregate(true, (l,r) => l && r),
				() => { setInstructionText("Winner!"); })
		};

		setInstructionText(stages [stage].instruction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
