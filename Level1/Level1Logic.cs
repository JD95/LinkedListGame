using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Stage {

	public Func<bool> progressCheck;
	public Action progress;
	public string instruction;

	public Stage (Func<bool> p, Action pg, string i){
		this.progressCheck = p;
		this.progress = pg;
		this.instruction = i;
	}
}

public class Level1Logic : WinCondition {

    public GameBoard board;

    public GameNode firstNode;

	public GameNode[] groupOfNodes = new GameNode[3];

	int stage = 0;
	List<Stage> stages;


    public override bool canProgress()
    {
		return (stage >= stages.Count) ? false : stages[stage].progressCheck();
    }

    public override void progress()
    {
		stages [stage++].progress ();
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
        Debug.Log("Please delete the node!");

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
			new Stage(
				() => !firstNode.isActiveAndEnabled,
				() => { 
					firstNode.deleteNode();
					foreach (var node in groupOfNodes)
						node.value.SetActive(true);
				}, "Please delete the node"),
			// Stage 2
			new Stage(
				() => groupOfNodes.Select(n => !n.value.activeSelf).Aggregate(true, (l,r) => l && r),
				() => { Debug.Log("Winner!"); }, "Please delete all of the nodes")
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
