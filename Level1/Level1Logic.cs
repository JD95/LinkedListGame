using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class Stage {

	public Func<bool> progressCheck;
	public Action setup;
	public string instruction;

	// Creates a new stage for the level
	public Stage (string instruction, Action setup, Func<bool> progressCondition){
		this.progressCheck = progressCondition;
		this.setup = setup;
		this.instruction = instruction;
	}
}

public class Level1Logic : WinCondition {

	public Text instructions;

    public GameBoard board;

    public GameNode firstNode;

	public GameNode[] groupOfNodes = new GameNode[3];

	int stage = -1; // Because we want to progress to stage 0
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
		stage++;
		stages [stage].setup ();
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
		
		stages = new List<Stage>(){
			// Stage 1
			new Stage( "Please delete the node", () => {
				firstNode = board.addNewNodeReturn ();

				for (int i = 0; i < 3; i++)
					groupOfNodes [i] = board.addNewNodeReturn ();

				firstNode.value.SetActive(true);

				foreach(var node in groupOfNodes)
					node.value.SetActive(false);

			}, () => !firstNode.isActiveAndEnabled),

			// Stage 2
			new Stage("Please delete all of the nodes",() => { 
				firstNode.deleteNode();
				foreach (var node in groupOfNodes)
					node.value.SetActive(true);
			}, () => groupOfNodes.Select(n => !n.value.activeSelf).Aggregate(true, (l,r) => l && r)),

			// Win state
			new Stage("Winner!", () => {}, () => false)
		};

		progress ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
